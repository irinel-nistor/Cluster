using Cluster2.Model.Tree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cluster2.Model.Tree
{
    class ThreadTree : IThreadTree<NodeModel>
    {
        private IThreadTree<NodeModel> tree;
        private Action<Thread> onThreadClose;

        public ThreadTree(IThreadTree<NodeModel> tree, Action<Thread> onThreadClose)
        {
            this.tree = tree;
            this.onThreadClose = onThreadClose;
        }

        public void Search()
        {
            this.tree.Search(false);
            onThreadClose(Thread.CurrentThread);
        }

        public void Search(bool option){ }

        public void AttachVisitor(Visitor.IVisitor visitor)
        {
            this.tree.AttachVisitor(visitor);
        }

        public bool MasterThread
        {
            get { return this.tree.MasterThread; }
            set { this.tree.MasterThread = value; }
        }
    }
}
