using Cluster2.Model.Tree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Search
{
    class DfsSearch<NodeModel> : ISearchCollection<NodeModel>
    {
        Stack<NodeModel> stack;
        public DfsSearch()
        {
            stack = new Stack<NodeModel>();
        }
        public void Push(NodeModel node)
        {
            stack.Push(node);
        }

        public int Count
        {
            get
            {
                return stack.Count;
            }
        }

        public NodeModel Peek()
        {
            return stack.Peek();
        }


        public NodeModel Pop()
        {
            return stack.Pop();
        }
    }
}
