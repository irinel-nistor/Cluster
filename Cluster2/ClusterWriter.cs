using Cluster2.Model.Points;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2
{
    class ClusterWriter
    {
        private Random random;
        private string filePath;

        public ClusterWriter(int index, string fileSufix)
        {
            var directory = "Read";
            Directory.CreateDirectory(directory);
            this.filePath = directory + "/" + fileSufix + index;
            random = new Random();
        }

        public ClusterWriter(string filePath)
        {
            this.filePath = filePath;
        }

        public void WriteCluster(int n, int m){
            var strBuilder = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++ )
                {
                    var randomNumber = random.Next(100);
                    strBuilder.Append(randomNumber + " ");
                }
                strBuilder.AppendLine();
            }
                       
            using (var streamWriter = new StreamWriter(this.filePath))
            {
                streamWriter.WriteLine(strBuilder.ToString());
            }
        }

        public Dictionary<int, Point> CreateMappingCoordinates()
        {
            int counter = 0;
            string line;
            var coordinates = new Dictionary<int, Point>();

            using (var file = new StreamReader(this.filePath))
            {
                while (!string.IsNullOrWhiteSpace((line = file.ReadLine())))
                {
                    double coordinate;
                    var point = new Point();
                    foreach (var elem in line.Split(new []{' ', '\t'}))
                    {
                        if (double.TryParse(elem, out coordinate))
                        {
                            point.Coordinates.Add(coordinate);
                        }
                    }
                    coordinates.Add(counter, point);
                    counter++;
                }
            }
            return coordinates;
        }

        public Dictionary<int, Point> CreateSortedMappingCoordinates()
        {
            int counter = 0;
            string line;
            var coordinates = new Dictionary<int, Point>();
            var coordinateList = new List<Point>();

            using (var file = new StreamReader(this.filePath))
            {
                while (!string.IsNullOrWhiteSpace((line = file.ReadLine())))
                {
                    double coordinate;
                    var point = new Point();
                    foreach (var elem in line.Split(new[] { ' ', '\t' }))
                    {
                        if (double.TryParse(elem, out coordinate))
                        {
                            point.Coordinates.Add(coordinate);
                        }
                    }
                    coordinateList.Add(point);
                }
            }

            var centerOfGravityPoint = this.CenterOfGravityPoint(coordinateList);
            var orderedCoordinates = coordinateList.OrderByDescending(point => point.DistanceToPoint(centerOfGravityPoint));
            foreach(var coordinate in orderedCoordinates){
                coordinates.Add(counter, coordinate);
                counter++;
            }

            return coordinates;
        }

        private Point CenterOfGravityPoint(List<Point> coordinateList){
            Point centerOfGravityPoint = null;
            var minAverage = double.MaxValue;

            foreach(var point in coordinateList){
                var averageDistanceToTheAllPoints = coordinateList.Average(otherPoint => otherPoint.DistanceToPoint(point));
                if(minAverage > averageDistanceToTheAllPoints){
                    minAverage = averageDistanceToTheAllPoints;
                    centerOfGravityPoint = point;
                }
            }

            return centerOfGravityPoint;
        }
    }
}
