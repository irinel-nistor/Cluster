using Cluster2.Model.Tree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Model.Visitor
{
    class AllClusterCountVisitor : IVisitor
    {
        public Dictionary<int, NodeModel> minSumPerClusterCount;

        public AllClusterCountVisitor()
        {
            minSumPerClusterCount = new Dictionary<int, NodeModel>();
        }

        public void Visit(NodeModel node)
        {
            var nodeClusterCount = node.Clusters.Count;
            if (minSumPerClusterCount.ContainsKey(nodeClusterCount))
            {
                if (minSumPerClusterCount[nodeClusterCount].SumOfSquares > node.SumOfSquares)
                {
                    minSumPerClusterCount[nodeClusterCount] = node;
                }
            }
            else
            {
                minSumPerClusterCount[nodeClusterCount] = node;
            }
        }

        public override string ToString()
        {
            var strBuilder = new StringBuilder();
            foreach (var pair in this.minSumPerClusterCount)
            {
                strBuilder.AppendFormat(" level {0} ----- {1}", pair.Key, pair.Value);
                strBuilder.AppendLine();
            }

            return strBuilder.ToString();
        }
    }
}
