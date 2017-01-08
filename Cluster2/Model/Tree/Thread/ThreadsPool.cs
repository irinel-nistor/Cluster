using Cluster2.Factory;
using Cluster2.Model.Tree.Node;
using Cluster2.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Cluster2.Model.Tree
{
    class ThreadsPool : IThreadPool
    {
        List<Tuple<Thread, IThreadTree<NodeModel>>> threadList;
        TreeFactory treeFactory;
        private int maxThreads;
        private ThreadCount threadCount;

        public ThreadsPool(TreeFactory treeFactory, ThreadCount threadCount)
        {
            this.treeFactory = treeFactory;
            threadList = new List<Tuple<Thread, IThreadTree<NodeModel>>();
            this.threadCount = threadCount;
            this.maxThreads = 200;
        }

        public bool TryNewThreadSearch(NodeModel node)
        {
            var hasAvailableThreads = HasAvailableThreads();
            if (hasAvailableThreads)
            {
                this.threadCount.Value ++;
                var tree = this.treeFactory.create(node, (thread) =>
                {
                    this.threadCount.Value --;
                });

                var oThread = new Thread(new ThreadStart(tree.Search));
                threadList.Add(new Tuple<Thread, IThreadTree<NodeModel>>(oThread, tree));
                oThread.Start();
            }

            return hasAvailableThreads;
        }

        public bool HasAvailableThreads()
        {
            return this.threadCount.Value < maxThreads;
        }

        public void WaitAll()
        {
            for (var i = 0; i < threadList.Count; i++)
            {
                var thread = threadList[i].Item1;
                threadList[i].Item2.MasterThread = true;
                thread.Join();
            }
        }
    }
}
