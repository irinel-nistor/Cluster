using Cluster2.Model.Tree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Model.Visitor
{
    class ClusterMinSumVIsitor:IVisitor
    {
        public ClusterMinSumVIsitor(int m)
        {
            this.NumberOfClusters = m;
            this.MinSumOfSquares = int.MaxValue;
        }
        public void Visit(NodeModel node)
        {
            if(node.Clusters.Count == this.NumberOfClusters && node.SumOfSquares < this.MinSumOfSquares){
                this.MinSumOfSquares = node.SumOfSquares;
                this.MinNode = node;
            }
        }

        public int NumberOfClusters { get; set; }

        public double MinSumOfSquares { get; set; }

        public NodeModel MinNode { get; set; }
    }
}
