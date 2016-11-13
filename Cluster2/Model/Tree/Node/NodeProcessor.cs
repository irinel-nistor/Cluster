using Cluster2.Model.Tree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Model.Tree
{
    class NodeProcessor:BaseNode
    {
        public NodeModel VidNode = new NodeModel();

        private IEnumerable<int> indexes;
        
        public NodeProcessor(IEnumerable<int> indexes, Points.SumOfSquareCalculator sumOfSquareCalculator):base(sumOfSquareCalculator)
        {
            this.indexes = indexes;
        }

        public NodeModel GenerateRoot()
        {
            var rootNode = new NodeModel();
            foreach (var index in indexes)
            {
                rootNode.Clusters.AddLast(new[] { index });
            }
            rootNode.Arrow = rootNode.Clusters.First.Next;
            rootNode.SumOfSquares = sumOfSquaresCalculator.CalculateSumOfSquares(rootNode);
            return rootNode;
        }

        public NodeModel GenerateNextChild(NodeModel node)
        {
            var childNode = new NodeModel();
            if (!HasMoreChildren(node))
            {
                return VidNode;
            }

            var iterator = node.Clusters.First;
            while (iterator != null)
            {
                // merge step
                if (node.currentToBeMerged.Equals(iterator))
                {
                    AddMergeNodeToChild(childNode, node);
                }
                // merged element should be ignored
                else if (!node.toBeMergedWith.Equals(iterator))
                {
                    childNode.Clusters.AddLast(iterator.Value); 
                    if (node.toBeMergedWith.Equals(iterator.Previous))
                    {
                        childNode.Arrow = childNode.Clusters.Last;
                        childNode.Bullet = childNode.Clusters.Count - 2;
                    }
                }
                iterator = iterator.Next;
            }
            // increment p 
            node.toBeMergedWith = node.toBeMergedWith.Next;
            if (node.toBeMergedWith == null)
            {
                node.currentToBeMerged = node.currentToBeMerged.Next;
                node.Bullet++;
            }
            return childNode;
        }

        private bool HasMoreChildren(NodeModel node)
        {
            if (node.currentToBeMerged == null)
            {
                node.currentToBeMerged = node.Clusters.First;
            }
            if (node.Arrow == node.currentToBeMerged)
            {
                node.Arrow = node.Arrow.Next;
            }
            if (node.toBeMergedWith == null)
            {
                node.toBeMergedWith = node.Arrow;
            }
            if (node.Arrow == null || node.currentToBeMerged == null || node.currentToBeMerged == node.Clusters.Last)
            {
                return false;
            }
            return true;
        }

        private void AddMergeNodeToChild(NodeModel childNode, NodeModel parentNode)
        {
            var mergedValue = MergeNode(parentNode);
            childNode.Clusters.AddLast(mergedValue);
            var mergedCluster = new MergedCluster
            {
                MergedClusterComponents = new[] { parentNode.currentToBeMerged.Value, parentNode.toBeMergedWith.Value },
                Value = mergedValue
            };
            childNode.SumOfSquares = this.sumOfSquaresCalculator.CalculateSumFromParent(mergedCluster, parentNode);
        }

        private int[] MergeNode(NodeModel node)
        {
            return node.currentToBeMerged.Value.Concat(node.toBeMergedWith.Value).ToArray();            
        }
    }
}
