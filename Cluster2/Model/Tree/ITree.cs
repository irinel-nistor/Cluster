using Cluster2.Model.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Model.Tree
{
    interface ITree<NodeModel>
    {
        void Search();

        void Search(bool masterThread);

        void AttachVisitor(IVisitor visitor);
    }
}
