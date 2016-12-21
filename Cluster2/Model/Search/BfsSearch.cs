using Cluster2.Model.Tree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Search
{
    class BfsSearch<NodeModel> : ISearchCollection<NodeModel>
    {
        Queue<NodeModel> queue;

        public BfsSearch()
        {
            queue = new Queue<NodeModel>();
        }
        public void Push(NodeModel node)
        {
            queue.Enqueue(node);
        }

        public int Count
        {
            get { return queue.Count; }
        }

        public NodeModel Peek()
        {
            return queue.Peek();
        }

        public NodeModel Pop()
        {
            return queue.Dequeue();
        }
    }
}
