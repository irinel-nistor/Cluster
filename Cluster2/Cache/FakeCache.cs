using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Cache
{
    class FakeCache: IClusterCache
    {
        public bool HasKey(int[] cluster)
        {
            return false;
        }

        public double GetValue(int[] cluster)
        {
            return 0.0;
        }

        public void CacheValue(int[] cluster, double value)
        {
            
        }

        public void RemoveCache(int[] cluster)
        {
        }

        public double GetOrCache(int[] mergedCluster, Func<int[], double> valueGenerator)
        {
            return valueGenerator(mergedCluster);
        }
    }
}
