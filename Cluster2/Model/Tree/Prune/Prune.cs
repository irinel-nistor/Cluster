using Cluster2.Model.Tree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Model.Tree.Prune
{
    class Prune: IPrune
    {
        private Dictionary<int, double> minSumPerClusterNumber;

        public Prune(int numberOfClusters)
        {
            minSumPerClusterNumber = new Dictionary<int, double>();
            for (var i = 0; i < numberOfClusters; i++ )
            {
                minSumPerClusterNumber[i] = double.MaxValue;
            }
        }

        public bool IsPrune(NodeModel node)
        {
            var prune = false;
            if (node.SumOfSquares > minSumPerClusterNumber[node.Bullet])
            {
                 prune = true;
            }

            var numberOfClusertsOfCurentNode = node.Clusters.Count;
            if (!prune && node.SumOfSquares < minSumPerClusterNumber[numberOfClusertsOfCurentNode - 1])
            {
                minSumPerClusterNumber[numberOfClusertsOfCurentNode - 1] = node.SumOfSquares;
            }

            return prune;
        }

        public override string ToString()
        {
            var strBuilder = new StringBuilder();
            foreach (var pair in this.minSumPerClusterNumber)
            {
                strBuilder.Append(string.Format(" level {0} ----- {1}", pair.Key, pair.Value));
                strBuilder.AppendLine();
            }

            return strBuilder.ToString();
        }
    }
}
