using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2
{
    class TimeStamp
    {
        public TimeSpan GetTimeStamp(Action function)
        {
            var stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            function();
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }
    }
}
