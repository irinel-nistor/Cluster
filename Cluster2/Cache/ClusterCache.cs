using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2
{
    class ClusterCache : IClusterCache
    {
        private Dictionary<int[], double> cache;

        public ClusterCache()
        {
            cache = new Dictionary<int[], double>();
        }

        public  bool HasKey(int[] cluster)
        {
            if (cluster.Count() == 1)
            {
                return true;
            }

          //  var cacheKey = ClusterToString(cluster);
            return cache.ContainsKey(cluster);
        }

        public  double GetValue(int[] cluster)
        {
            if(cluster.Count() == 1){
                return 0;
            }

       //     var cacheKey = ClusterToString(cluster);
            return cache[cluster];
        }

        public  void CacheValue(int[] cluster, double value)
        {
            if (cluster.Count() == 1)
            {
                return;
            }

       //     var cacheKey = ClusterToString(cluster);
            cache[cluster] = value;
        }

        public  void RemoveCache(int[] cluster)
        {
           // var cacheKey = ClusterToString(cluster);
            cache.Remove(cluster);
        }

        public double GetOrCache(int[] mergedCluster, Func<int[], double> valueGenerator)
        {
            var mergedClusterResidualSum = 0.0;
            if (this.HasKey(mergedCluster))
            {
                mergedClusterResidualSum = this.GetValue(mergedCluster);
            }
            else
            {
                mergedClusterResidualSum = valueGenerator(mergedCluster);
                this.CacheValue(mergedCluster, mergedClusterResidualSum);
            }

            return mergedClusterResidualSum;
        }

        private  string ClusterToString(int[] cluster)
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append("{");
            var innerSeparator = "";
            foreach (var item in cluster)
            {
                strBuilder.Append(string.Format("{0}{1}", innerSeparator, item));
                innerSeparator = ",";
            }

            strBuilder.Append("}");
            return strBuilder.ToString();
        }
    }
}
