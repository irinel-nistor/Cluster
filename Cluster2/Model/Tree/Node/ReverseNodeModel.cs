using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Model.Tree.Node
{
    class ReverseNodeModel : INodeModel
    {
        public List<int[]> Clusters { get; set; }

        public int[] MergedCluster { get; set; }

        public bool Initialized;

        public Queue<List<int[]>> SeedNodes;

        public int Bullet { get; set; }

        public ReverseNodeModel()
        {
            Clusters = new List<int[]>();
            SeedNodes = new Queue<List<int[]>>();
            Initialized = false;
        }

        public int Iterator;

        public double SumOfSquares { get; set; }

        public override string ToString()
        {
            var strBuilder = new StringBuilder();
            var outerSeparator = "";
            foreach (var cluster in Clusters)
            {
                strBuilder.Append(string.Format("{0}{1}", outerSeparator, "{"));
                var innerSeparator = "";
                foreach (var item in cluster)
                {
                    strBuilder.Append(string.Format("{0}{1}", innerSeparator, item));
                    innerSeparator = ",";
                }

                strBuilder.Append("}");
                outerSeparator = ",";
            }

            strBuilder.Append(" => " + this.SumOfSquares);
            strBuilder.Append("\n");
            return strBuilder.ToString();
        }
    }
}
