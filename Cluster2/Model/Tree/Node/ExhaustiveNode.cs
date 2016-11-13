using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Model.Tree.Node
{
    class ExhaustiveNode : BaseNode
    {
        private Points.SumOfSquareCalculator sumOfSquareCalculator;

        public ExhaustiveNode(Points.SumOfSquareCalculator sumOfSquareCalculator) : base(sumOfSquareCalculator) { }

        protected override BaseNode GenerateNextChild()
        {
            var child = new ExhaustiveNode(this.sumOfSquaresCalculator);
            if (!HasMoreChildren())
            {
                return VidNode;
            }

            var iterator = Clusters.First;
            while (iterator != null)
            {
                // merge step
                if (currentToBeMerged.Equals(iterator))
                {
                    AddMergeNodeToChild(child);
                }
                // merged element should be ignored
                else if (!toBeMergedWith.Equals(iterator))
                {
                    child.AddCluster(iterator.Value);
                    if (toBeMergedWith.Equals(iterator.Previous))
                    {
                        //       child.Arrow = child.Clusters.Last;
                    }
                }
                iterator = iterator.Next;
            }
            // increment p 
            toBeMergedWith = toBeMergedWith.Next;
            if (toBeMergedWith == null)
            {
                currentToBeMerged = currentToBeMerged.Next;
            }
            Children.Add(child);
            return child;
        }

        protected override bool HasMoreChildren()
        {
            if (currentToBeMerged == null)
            {
                currentToBeMerged = Clusters.First;
            }

            if (currentToBeMerged == null || currentToBeMerged == Clusters.Last)
            {
                return false;
            }
            return true;
        }

        protected override int[] MergeNode()
        {
            throw new NotImplementedException();
        }
    }
}
