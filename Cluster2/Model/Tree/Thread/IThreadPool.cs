using Cluster2.Model.Tree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Model.Tree
{
    interface IThreadPool
    {
        bool TryNewThreadSearch(NodeModel node);

        bool HasAvailableThreads();

        void WaitAll();
    }
}
