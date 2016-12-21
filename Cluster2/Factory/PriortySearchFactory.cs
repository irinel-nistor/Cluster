using Cluster2.Model.Search;
using Cluster2.Model.Tree;
using Cluster2.Model.Tree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Factory
{
    class PrioritySearchFactory<NodeModel> where NodeModel : INodeModel
    {
        public ISearchCollection<NodeModel> create()
        {
            return new PrioritySearch<NodeModel>(new SumNodeComparer<NodeModel>());
        }
    }
}
