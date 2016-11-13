using Cluster2.Model.Tree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Model.Visitor
{
    class LogVisitor : IVisitor
    {
        public void Visit(NodeModel node)
        {
            Console.Write(node.ToString());
            Console.WriteLine();
        }
    }
}
