using Cluster2.Model.Tree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.PowerCollections;

namespace Cluster2.Model.Search
{
    class ExtendedPrioritySearch<NodeModelModel> : ISearchCollection<NodeModelModel>
    {
        OrderedBag<NodeModelModel> priorityQueue;
        Stack<OrderedBag<NodeModelModel>> priorityCache;
        IComparer<NodeModelModel> nodeSumComparer;
        
        public ExtendedPrioritySearch(IComparer<NodeModelModel> nodeSumComparer)
        {
            priorityQueue = new OrderedBag<NodeModelModel>(nodeSumComparer);
            priorityCache = new Stack<OrderedBag<NodeModelModel>>();
            this.nodeSumComparer = nodeSumComparer;
        }

        public void Push(NodeModelModel node)
        {
            if (priorityQueue.Count >= 300)
            {
                var cachedQueue = new OrderedBag<NodeModelModel>(nodeSumComparer);
                for (int i = 0; i < 100; i++ )
                {
                    cachedQueue.Add(priorityQueue.RemoveFirst());
                }

                priorityCache.Push(priorityQueue);
                priorityQueue = cachedQueue;
            }

            priorityQueue.Add(node);
        }

        public int Count
        {
            get { return priorityQueue.Count; }
        }

        public NodeModelModel Peek()
        {
            return priorityQueue.First();
        }

        public NodeModelModel Pop()
        {
            var removedNode = priorityQueue.RemoveFirst();
            if(priorityQueue.Count == 0 && priorityCache.Count > 0){
                priorityQueue = priorityCache.Pop();
            }

            return removedNode;
        }
    }
}
