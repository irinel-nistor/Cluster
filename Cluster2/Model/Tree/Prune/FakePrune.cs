using Cluster2.Model.Tree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Model.Tree.Prune
{
    class FakePrune : IPrune
    {
        public bool IsPrune(NodeModel node)
        {
            return false;
        }
    }
}
