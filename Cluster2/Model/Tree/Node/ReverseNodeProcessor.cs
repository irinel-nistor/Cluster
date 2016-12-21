using Cluster2.Model.Points;
using Cluster2.Model.Tree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Model.Tree
{
    class ReverseNodeProcessor : INodeProcessor<ReverseNodeModel>
    {
        public static ReverseNodeModel VidNode = new ReverseNodeModel();
        private SumOfSquareCalculator sumOfSquaresCalculator;
        private IEnumerable<int> indexes;

        public ReverseNodeProcessor(IEnumerable<int> indexes, Points.SumOfSquareCalculator sumOfSquaresCalculator)
        {
            this.indexes = indexes;
            this.sumOfSquaresCalculator = sumOfSquaresCalculator;
        }

        public ReverseNodeModel GenerateRoot()
        {
            var rooNodeModel = new ReverseNodeModel();
            rooNodeModel.Clusters.Add(indexes.ToArray());

            rooNodeModel.SumOfSquares = this.sumOfSquaresCalculator.ResidualSumOfSquares(rooNodeModel.Clusters.First());
            return rooNodeModel;
        }

        public ReverseNodeModel GenerateNextChild(ReverseNodeModel node)
        {
            var childNode = new ReverseNodeModel();

            childNode.Clusters = node.Clusters.ToList();
            childNode.SumOfSquares = node.SumOfSquares;

            var lastCluster = childNode.Clusters.Last();
            childNode.Clusters.RemoveAt(childNode.Clusters.Count - 1);
            childNode.SumOfSquares -= sumOfSquaresCalculator.ResidualSumOfSquares(lastCluster);

            List<int[]> subSetGroup;
            if(node.Initialized){
                subSetGroup = GenerateNextSubSetGroup(node);   
            }
            else
            {
                subSetGroup = GenerateFirstSubGroup(node);
                node.Initialized = true;
            }

            if (subSetGroup != null)
            {
                foreach(var subSet in subSetGroup){
                    childNode.Clusters.Add(subSet);
                    childNode.SumOfSquares += sumOfSquaresCalculator.ResidualSumOfSquares(subSet);
                }

                return childNode;
            }

            return null;
        }

        private List<int[]> GenerateNextSubSetGroup(ReverseNodeModel node)
        {
            var clusterGroup = new List<int[]>();

            var seedNode = GetNextSeedSet(node);

            if (seedNode != null)
            {
                var secondSeedSubset = seedNode[1];
   
                var firstSeedSubset = seedNode[0];
                var firstNewSubset = firstSeedSubset.ToList();
                var secondNewSubset = new List<int>();
                for (var i = 0; i < secondSeedSubset.Count();i ++ )
                {
                    if(i == node.Iterator){
                        firstNewSubset.Add(secondSeedSubset[i]);
                    }
                    else
                    {
                        secondNewSubset.Add(secondSeedSubset[i]);
                    }
                }

                node.Iterator++;
                clusterGroup.Add(firstNewSubset.ToArray());
                clusterGroup.Add(secondNewSubset.ToArray());
                node.SeedNodes.Enqueue(clusterGroup);
                return clusterGroup;               
            }

            return null; 
        }

        private List<int[]> GetNextSeedSet(ReverseNodeModel node)
        {
            var seedNode = node.SeedNodes.Peek();
            var secondSeedSubset = seedNode[1];
            var firstSeedSubset = seedNode[0];

            while ( node.Iterator >= secondSeedSubset.Count() || secondSeedSubset[node.Iterator] <= firstSeedSubset.Last() || secondSeedSubset.Count() <= 1)
            {
                node.Iterator ++;
                if (node.Iterator >= secondSeedSubset.Count() || secondSeedSubset.Count() < 2)
                {
                    node.Iterator = 0;
                    node.SeedNodes.Dequeue();

                    if (node.SeedNodes.Count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        seedNode = node.SeedNodes.Peek();
                        secondSeedSubset = seedNode[1];
                        firstSeedSubset = seedNode[0];
                    }
                }

            }

            return seedNode;
        }

        private List<int[]> GenerateFirstSubGroup(ReverseNodeModel node)
        {
            var clusterGroup = new List<int[]>();

            var lastCluster = node.Clusters.Last();
            var firstNewSubset = new List<int>();
            var secondNewSubset = new List<int>();
            for (var i = 0; i < lastCluster.Count(); i++)
            {
                if (i == 0)
                {
                    firstNewSubset.Add(lastCluster[i]);
                }
                else
                {
                    secondNewSubset.Add(lastCluster[i]);
                }
            }

            clusterGroup.Add(firstNewSubset.ToArray());
            clusterGroup.Add(secondNewSubset.ToArray());
            node.SeedNodes.Enqueue(clusterGroup);
            return clusterGroup;
        }

        public bool HasMoreChildren(ReverseNodeModel node)
        {
            if ((node.Initialized && node.SeedNodes.Count == 0) || node.Clusters.Last().Count() <= 1)
            {
                return false;
            }

            return true;
        }
    }
}
