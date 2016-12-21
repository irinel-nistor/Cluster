using Cluster2.Factory;
using Cluster2.Model.Tree.Node;
using Cluster2.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cluster2.Model.Tree
{
    class ThreadsPool 
    {
        HashSet<Thread> threadList;
        TreeFactory treeFactory;
        private int maxThreads;
        private bool masterThreadWaits = false;

        public ThreadsPool(TreeFactory treeFactory)
        {
            this.treeFactory = treeFactory;
            threadList = new HashSet<Thread>();
            this.maxThreads = 10;
        }

        public void TryNewThreadSearch(NodeModel node)
        {
            if (threadList.Count < maxThreads)
            {
                var searchCollection = new DfsSearch<NodeModel>();
                searchCollection.Push(node);

                var tree = this.treeFactory.create(searchCollection, (thread) => { 
                    if(!this.masterThreadWaits){
                        this.threadList.Remove(thread); 
                    }
                });

                var oThread = new Thread(new ThreadStart(tree.Search));
                threadList.Add(oThread);
                oThread.Start();
            }
        }

        public void WaitAll()
        {
            this.masterThreadWaits = true;
            foreach(var thread in threadList){
                thread.Join();
            }
        }
    }
}
