using Cluster2.Model.Tree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.PowerCollections;

namespace Cluster2.Model.Search
{
    class PrioritySearch<NodeModelModel> : ISearchCollection<NodeModelModel>
    {
        OrderedBag<NodeModelModel> priorityQueue;

        public PrioritySearch(IComparer<NodeModelModel> nodeSumComparer)
        {
            priorityQueue = new OrderedBag<NodeModelModel>(nodeSumComparer);
        }

        public void Push(NodeModelModel node)
        {
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
            return priorityQueue.RemoveFirst();
        }
    }
}
