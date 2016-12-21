using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Model.Points
{
    public class Point
    {
        public Point()
        {
            Coordinates = new List<double>();
        }

        public IList<double> Coordinates { get; set; }

        public double DistanceToPoint(Point pointB)
        {
            double distance = 0;
            var numberOfCoordinates = Coordinates.Count;
            if(numberOfCoordinates != pointB.Coordinates.Count){
                throw new ArgumentException("The distance can't be calculated, as the points are of different spaces");
            }
            for (var index = 0; index < numberOfCoordinates; index++)
            {
                distance += Square(Coordinates[index] - pointB.Coordinates[index]);
            }

            return distance;
        }

        private double Square(double p)
        {
            return p * p;
        }
    }
}
