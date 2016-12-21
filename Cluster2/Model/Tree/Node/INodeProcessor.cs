using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Model.Tree.Node
{
    interface INodeProcessor<NodeModel> 
    {
        bool HasMoreChildren(NodeModel node);
        
        NodeModel GenerateRoot();

        NodeModel GenerateNextChild(NodeModel node);
    }
}
