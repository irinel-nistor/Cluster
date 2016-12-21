                                                                                                                using Cluster2.Model.Tree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Model.Tree
{
    class SumNodeComparer<NodeModel> : IComparer<NodeModel> where NodeModel : INodeModel
    {
        public int Compare(NodeModel x, NodeModel y)
        {
            if(y == null && x == null){
                return 0;
            }
            if(y == null){
                return 1;
            }
            if(x == null){
                return -1;
            }
            var comparisonResult = x.SumOfSquares.CompareTo(y.SumOfSquares);
            if (comparisonResult == 0)
            {
                comparisonResult = x.Clusters.Count.CompareTo(y.Clusters.Count);
            }
            return comparisonResult;
        }
    }
}
