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
    class ThreadsPoolFrame : IThreadPool
    {
        TreeFactory treeFactory;
        private int maxThreads;
        private bool masterThreadWaits = false;
        private HashSet<AutoResetEvent> eventsList;

        public ThreadsPoolFrame(TreeFactory treeFactory)
        {
            this.treeFactory = treeFactory;
            eventsList = new HashSet<AutoResetEvent>();
        }

        public bool TryNewThreadSearch(NodeModel node)
        {
            var hasAvailableThreads = HasAvailableThreads();
            if (hasAvailableThreads)
            {
                var tree = this.treeFactory.create(node, (thread) => { 
                    
                });

                var autoResetEvent = new AutoResetEvent(false);
                eventsList.Add(autoResetEvent);
                ThreadPool.QueueUserWorkItem(new WaitCallback((obj) => { tree.Search(); autoResetEvent.Set();
                    if(!this.masterThreadWaits){
                        eventsList.Remove(autoResetEvent); 
                    }
                }), autoResetEvent);
            }

            return hasAvailableThreads;
        }

        public bool HasAvailableThreads()
        {
            var threadCount = 0;
            var j = 0;
            ThreadPool.GetAvailableThreads(out threadCount, out j);
            return threadCount > 0;
        }

        public void WaitAll()
        {
            this.masterThreadWaits = true;
            foreach(var autoEvent in eventsList){
                autoEvent.WaitOne();
            }
        }
    }
}
