using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Model.Tree
{
    class MergedCluster
    {
        public IEnumerable<int> Value { get; set; }

        public IEnumerable<int[]> MergedClusterComponents { get; set; }
    }
}
