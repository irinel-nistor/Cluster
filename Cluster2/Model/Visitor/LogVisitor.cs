using Cluster2.Model.Tree.Node;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Model.Visitor
{
    class LogVisitor : IVisitor
    {
        private StreamWriter writer;

        public LogVisitor(StreamWriter writer)
        {
            this.writer = writer;
        }

        public void Visit(INodeModel node)
        {
            this.writer.Write(node.ToString());
            this.writer.WriteLine();
        }
    }
}
