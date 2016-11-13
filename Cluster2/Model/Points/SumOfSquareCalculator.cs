using Cluster2.Model.Points;
using Cluster2.Model.Tree;
using Cluster2.Model.Tree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Model.Points
{
    class SumOfSquareCalculator
    {
        private Space space;

        public SumOfSquareCalculator(Space space)
        {
            this.space = space;
        }

        public double CalculateSumFromParent(MergedCluster mergedCluster, NodeModel parent)
        {
            var mergedClusterResidualSum = space.ResidualSumOfSquares(mergedCluster.Value);
            mergedClusterResidualSum += parent.SumOfSquares;

            foreach (var clustersToBeRemovedFormResidualSum in mergedCluster.MergedClusterComponents)
            {
                mergedClusterResidualSum -= space.ResidualSumOfSquares(clustersToBeRemovedFormResidualSum);
            }

            return mergedClusterResidualSum;
        }

        public double CalculateSumOfSquares(NodeModel child)
        {
            var sumOfSquares = 0.0;
            foreach (var cluster in child.Clusters)
            {
                sumOfSquares += space.ResidualSumOfSquares(cluster);
            }
            return sumOfSquares;
        }
    }
}
