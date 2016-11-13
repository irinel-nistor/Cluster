using Cluster2.Model.Tree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Model.Visitor
{
    class ClusterNumberVisitor:IVisitor
    {
        int NrOfClusters { get; set; }

        public ClusterNumberVisitor(int nr)
        {
            this.NrOfClusters = nr;
        }

        public void Visit(NodeModel node)
        {
            if (node.Clusters.Count == this.NrOfClusters)
            {
                Console.WriteLine(node);
            }
        }
    }
}
