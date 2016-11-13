using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Model.Tree.Node
{
    class NodeModel
    {
        public NodeModel(){
            Clusters = new List<int[]>();
        }

        public int currentToBeMerged { get; set; }
        public int toBeMergedWith { get; set; }
        
        public List<int[]> Clusters;

        public int Bullet { get; set; }

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
