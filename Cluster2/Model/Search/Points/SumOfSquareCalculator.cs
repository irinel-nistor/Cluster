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

        public double ResidualSumOfSquares(int[] cluster)
        {
            var residualSumOfSquares = 0.0;

            if (cluster.Count() <= 1)
            {
                return residualSumOfSquares;
            }

            var centroid = CenterOfGravity(cluster);
            foreach (var pointIndex in cluster)
            {
                residualSumOfSquares += space.PointMapping[pointIndex].DistanceToPoint(centroid);
            }

            return residualSumOfSquares;
        }

        protected Point CenterOfGravity(int[] cluster)
        {
            var centroid = new Point();
            for (var index = 0; index < space.Dimensions; index++)
            {
                var indexCoordinate = cluster.Average(point => space.PointMapping[point].Coordinates[index]);
                centroid.Coordinates.Add(indexCoordinate);
            }
            return centroid;
        }
    }
}
