using Cluster2.Model.Tree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Model.Visitor
{
    class ClusterCountVizitor : IVisitor
    {
        private int numberOfNodes;

        public ClusterCountVizitor()
        {
            numberOfNodes = 0;
        }

        public void Visit(NodeModel node)
        {
            numberOfNodes++;
        }

        public override string ToString()
        {
            return numberOfNodes.ToString();
        }
    }
}
