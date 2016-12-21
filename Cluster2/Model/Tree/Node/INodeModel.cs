using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Model.Tree.Node
{
    interface INodeModel
    {
        List<int[]> Clusters {get;set;}

        double SumOfSquares { get; set; }

        int Bullet { get; set; }
    }
}
