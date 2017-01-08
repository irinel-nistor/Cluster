using Cluster2.Factory;
using Cluster2.Model.Points;
using Cluster2.Model.Points;
using Cluster2.Model.Search;
using Cluster2.Model.Tree;
using Cluster2.Model.Tree.Node;
using Cluster2.Model.Tree.Prune;
using Cluster2.Model.Visitor;
using Cluster2.Search;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cluster2
{
    class Program
    {
        static void Main(string[] args)
        {
            NormalRun();
        }

        private static void NormalRun()
        {
            Directory.CreateDirectory("Write");
            var sum = 0;
            var elapsed = new TimeSpan();
            var T = 10;
            for (int i = 1; i <= T; i++)
            {
                int n = 40, m = 1;
                var clusterCountVizitor = new ClusterCountVizitor();
                var allClusterVizitor = new AllClusterCountVisitor();
                using (var spreadSheet = new XslWriter("Write/optim6.xls", T, n, "multi thread mix no4"))
                {
                    try
                    {
                        var clusterWriter = new ClusterWriter(i ,"v2cluster40");
                        clusterWriter.WriteCluster(n, m);

                        var coordinates = clusterWriter.CreateMappingCoordinates();
                        var treeFactory = new TreeFactory(coordinates);

                        treeFactory.AttachVisitor(clusterCountVizitor);
                       // treeFactory.AttachVisitor(allClusterVizitor);

                        var tree = treeFactory.create();

                        var timeElapsed = new TimeStamp().GetTimeStamp(() => tree.Search());

                        spreadSheet.write("A" + i, int.Parse(clusterCountVizitor.ToString()));
                        spreadSheet.write("B" + i, timeElapsed.TotalSeconds);
                        spreadSheet.write("C" + i, treeFactory.ToString());
                        spreadSheet.write("D" + i, timeElapsed);
                        //spreadSheet.write("D" + i, allClusterVizitor.ToString());

                        elapsed = elapsed.Add(timeElapsed);
                        sum += Int32.Parse(clusterCountVizitor.ToString());
                    }
                    catch (Exception err)
                    {
                        spreadSheet.write("A" + i, err.Message);
                        spreadSheet.write("B" + i, clusterCountVizitor.ToString());
                    }
                }

                using (var stremWriter = new StreamWriter("Write/limit3", true))
                {
                    stremWriter.WriteLine();
                    stremWriter.WriteLine(sum / T);
                    stremWriter.WriteLine(new TimeSpan(elapsed.Ticks / T));
                }
            }
        }
        
        private static void GetMN(out int m)
        {
            Console.WriteLine("Enter the number of clusters:");
            m = int.Parse(Console.ReadLine());
        }
    }
}
