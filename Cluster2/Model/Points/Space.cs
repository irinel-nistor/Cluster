using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Model.Points
{
    class Space
    {
        public Dictionary<int, Point> PointMapping;

        public int Dimensions { get; private set; }

        public Space(Dictionary<int, Point> PointMapping)
        {
            this.Dimensions = GetDimension(PointMapping);
            this.PointMapping = PointMapping;
        }

        public double ResidualSumOfSquares(IEnumerable<int> pointIndexes)
        {
            var residualSumOfSquares = 0.0;
            
            if(pointIndexes.Count() == 0){
                return residualSumOfSquares;
            }
            
            var centroid = CenterOfGravity(pointIndexes);
            foreach (var pointIndex in pointIndexes)
            {
                residualSumOfSquares += PointMapping[pointIndex].DistanceToPoint(centroid);
            }

            return residualSumOfSquares;
        }

        protected Point CenterOfGravity(IEnumerable<int> pointIndexes)
        {
            var centroid = new Point();
            for (var index = 0; index < Dimensions; index++)
            {
                var indexCoordinate = pointIndexes.Average(point => PointMapping[point].Coordinates[index]);
                centroid.Coordinates.Add(indexCoordinate);
            }
            return centroid;
        }

        private int GetDimension(Dictionary<int, Point> PointMapping)
        {
            var pointsGroupedByCount = PointMapping.Values.GroupBy(point => point.Coordinates.Count);
            if (pointsGroupedByCount.Count() != 1)
            {
                throw new ArgumentException("The points should be of the same dimension");
            }
            return pointsGroupedByCount.FirstOrDefault().Key;
        }
    }
}
