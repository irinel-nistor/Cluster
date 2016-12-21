using Cluster2.Model.Tree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Model.Tree
{
    class NodeProcessor : INodeProcessor<NodeModel>
    {
        public static NodeModel VidNode = new NodeModel();
        private Points.SumOfSquareCalculator sumOfSquareCalculator;
        private IEnumerable<int> indexes;

        public NodeProcessor(IEnumerable<int> indexes, Points.SumOfSquareCalculator sumOfSquareCalculator)
        {
            this.indexes = indexes;
            this.sumOfSquareCalculator = sumOfSquareCalculator;
        }

        public NodeModel GenerateRoot()
        {
            var rooNodeModel = new NodeModel();
            foreach (var index in indexes)
            {
                rooNodeModel.Clusters.Add(new[] { index });
            }

            rooNodeModel.SumOfSquares = 0;
            return rooNodeModel;
        }

        public NodeModel GenerateNextChild(NodeModel node)
        {
            lock (node)
            {
                var childNode = new NodeModel();
                if (!HasMoreChildren(node))
                {
                    return VidNode;
                }

                this.InitSecondClusterToBeMerged(node);

                foreach (var cluster in node.Clusters)
                {
                    // merge step
                    if (node.Clusters[node.FirstClusterToBeMerged].Equals(cluster))
                    {
                        var mergedCluster = AddMergedClusterToChild(childNode, node);

                        this.CalculateSumFromParent(mergedCluster, node, childNode);
                    }
                    else if (!node.Clusters[node.SecondClusterToBeMerged].Equals(cluster))
                    {
                        childNode.Clusters.Add(cluster);
                    }
                    else
                    {
                        // merged element should be ignored
                        childNode.Bullet = childNode.Clusters.Count - 1;
                    }
                }

                // increment p 
                node.SecondClusterToBeMerged++;
                if (node.Clusters.Count <= node.SecondClusterToBeMerged)
                {
                    node.FirstClusterToBeMerged++;
                }

                return childNode;
            }
        }

        public bool HasMoreChildren(NodeModel node)
        {
            if (node.Bullet >= node.Clusters.Count - 1 || node.FirstClusterToBeMerged >= node.Clusters.Count - 1)
            {
                return false;
            }

            return true;
        }

        private void CalculateSumFromParent(int[] mergedCluster, NodeModel parent, NodeModel childNode)
        {
            var mergedClusterResidualSum = this.sumOfSquareCalculator.ResidualSumOfSquares(mergedCluster);
            mergedClusterResidualSum += parent.SumOfSquares;

            Func<int[], double> generator = this.sumOfSquareCalculator.ResidualSumOfSquares;
            if (parent.Clusters.Count() - parent.Bullet > 3)
            {
                mergedClusterResidualSum -= parent.Cache.GetOrCache(parent.Clusters[parent.FirstClusterToBeMerged], generator);
                mergedClusterResidualSum -= parent.Cache.GetOrCache(parent.Clusters[parent.SecondClusterToBeMerged], generator);
            }
            else
            {
                mergedClusterResidualSum -= generator(parent.Clusters[parent.FirstClusterToBeMerged]);
                mergedClusterResidualSum -= generator(parent.Clusters[parent.SecondClusterToBeMerged]);
            }

            childNode.SumOfSquares = mergedClusterResidualSum;
        }

        private int[] AddMergedClusterToChild(NodeModel childNode, NodeModel parenNodeModel)
        {
            var mergedCluster = GetMergeCluster(parenNodeModel);
            childNode.Clusters.Add(mergedCluster);

            return mergedCluster;
        }

        private int[] GetMergeCluster(NodeModel node)
        {
            var firstToBeMergedCluster = node.Clusters[node.FirstClusterToBeMerged];
            var secondToBeMergedCluster = node.Clusters[node.SecondClusterToBeMerged];
            return firstToBeMergedCluster.Concat(secondToBeMergedCluster).ToArray();
        }

        private void InitSecondClusterToBeMerged(NodeModel node){
            if(node.SecondClusterToBeMerged >= node.Clusters.Count || node.SecondClusterToBeMerged <= node.FirstClusterToBeMerged){
                if(node.Bullet >= node.FirstClusterToBeMerged){
                    node.SecondClusterToBeMerged = node.Bullet + 1;
                }
                else
                {
                    node.SecondClusterToBeMerged = node.FirstClusterToBeMerged + 1;
                }
            }
        }
    }
}
