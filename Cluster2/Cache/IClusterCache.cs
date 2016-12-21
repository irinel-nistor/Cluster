using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2
{
    interface IClusterCache
    {
          bool HasKey(int[] cluster);
      
          double GetValue(int[] cluster);
     
          void CacheValue(int[] cluster, double value);
     
          void RemoveCache(int[] cluster);

          double GetOrCache(int[] mergedCluster, Func<int[], double> valueGenerator);
    }
}
