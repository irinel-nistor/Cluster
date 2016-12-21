using Cluster2.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Model.Tree.Node
{
    class NodeModel : INodeModel
    {
        public NodeModel(){
            Clusters = new List<int[]>();
            Cache = new ClusterCache();
        }

        public IClusterCache Cache { get; set; }

        public int FirstClusterToBeMerged { get; set; }
        public int SecondClusterToBeMerged { get; set; }
        
        public List<int[]> Clusters {get;set;}

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
