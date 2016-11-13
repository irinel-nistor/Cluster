using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2
{
    interface ISearchCollection<NodeType>
    {
        void Push(NodeType node);

        int Count { get; }

        NodeType Peek();

        NodeType Pop();
    }
}
