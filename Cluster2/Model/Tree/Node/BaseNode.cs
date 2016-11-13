using Cluster2.Model.Points;
using Cluster2.Model.Tree;
using Cluster2.Model.Tree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2
{
    abstract class BaseNode
    {
        public BaseNode(SumOfSquareCalculator sumOfSquaresCalculator)
        {
            this.sumOfSquaresCalculator = sumOfSquaresCalculator;
        }

        protected SumOfSquareCalculator sumOfSquaresCalculator;
    }
}
