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
                    var randomNumber = random.Next(101);
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
                    int coordinate;
                    var point = new Point();
                    foreach (var elem in line.Split(' '))
                    {
                        if (int.TryParse(elem, out coordinate))
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
    }
}
