using Cluster2.Model.Tree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.PowerCollections;

namespace Cluster2.Model.Search
{
    class PrioritySearch : ISearchCollection<NodeModel>
    {
        OrderedBag<NodeModel> priorityQueue;

        public PrioritySearch(IComparer<NodeModel> nodeSumComparer)
        {
            priorityQueue = new OrderedBag<NodeModel>(nodeSumComparer);
        }

        public void Push(NodeModel node)
        {
            priorityQueue.Add(node);
        }

        public int Count
        {
            get { return priorityQueue.Count; }
        }

        public NodeModel Peek()
        {
            return priorityQueue.First();
        }

        public NodeModel Pop()
        {
            var minNode = priorityQueue.First();
            priorityQueue.Remove(minNode);
            return minNode;
        }
    }
}
