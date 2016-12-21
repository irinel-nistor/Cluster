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
