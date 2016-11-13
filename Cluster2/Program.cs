using Cluster2.Factory;
using Cluster2.Model.Points;
using Cluster2.Model.Points;
using Cluster2.Model.Search;
using Cluster2.Model.Tree;
using Cluster2.Model.Tree.Prune;
using Cluster2.Model.Visitor;
using Cluster2.Search;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2
{
    class Program
    {
        static void Main(string[] args)
        {
            Directory.CreateDirectory("Write");
            var sum = 0;
            var elapsed = new TimeSpan();
            using (var stremWriter = new StreamWriter("Write/dfs30T3"))
            {
                var T = 3;
                for (int i = 1; i <= T; i++)
                {
                    var clusterCountVizitor = new ClusterCountVizitor();
                    try
                    {
                        int n = 40, m = 1;

                        var clusterWriter = new ClusterWriter(i, "space");
                        clusterWriter.WriteCluster(n, m);
                        var coordinates = clusterWriter.CreateMappingCoordinates();

                        var space = new Space(coordinates);

                        var sumCulculator = new SumOfSquareCalculator(space);
                        var prune = new Prune(space.PointMapping.Count);
                        //var prune = new FakePrune();
                        var tree = new Tree(new DfsSearch(), new NodeProcessor(coordinates.Keys, sumCulculator), prune);

                        tree.AttachVisitor(clusterCountVizitor);

                        var timeElapsed = new TimeStamp().GetTimeStamp(() => tree.Search());

                        stremWriter.WriteLine(clusterCountVizitor.ToString());
                        stremWriter.WriteLine(timeElapsed);
                        
                         sum += Int32.Parse(clusterCountVizitor.ToString());
                         elapsed = elapsed.Add(timeElapsed);
                    }
                    catch (Exception err)
                    {
                        stremWriter.WriteLine(err.Message);
                        stremWriter.WriteLine(clusterCountVizitor.ToString());
                    }

                }

                stremWriter.WriteLine();
                stremWriter.WriteLine(sum / T);
                stremWriter.WriteLine(new TimeSpan(elapsed.Ticks / T));
            }
        }

        private static void GetMN(out int m)
        {
            Console.WriteLine("Enter the number of clusters:");
            m = int.Parse(Console.ReadLine());
        }
    }
}
