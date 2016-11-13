using Cluster2.Model.Points;
using Cluster2.Model.Tree;
using Cluster2.Model.Tree.Node;
using Cluster2.Model.Tree.Prune;
using Cluster2.Model.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2
{
    class Tree
    {
        private ISearchCollection<NodeModel> searchCollection;
        private IPrune prune;
        private IList<IVisitor> visitors;
        private NodeProcessor nodeProcessor;

        public Tree(ISearchCollection<NodeModel> searchMode, NodeProcessor nodeProcessor, IPrune prune)
        {
            this.searchCollection = searchMode;
            this.visitors = new List<IVisitor>();
            this.prune = prune;
            this.nodeProcessor = nodeProcessor;
        }

        public void Search()
        {
            var rootNode = this.nodeProcessor.GenerateRoot();
            PushNode(rootNode);
            while (searchCollection.Count != 0)
            {
                var currentNode = searchCollection.Peek();
                var childNode = this.nodeProcessor.GenerateNextChild(currentNode);
                if (childNode.Equals(this.nodeProcessor.VidNode))
                {
                    searchCollection.Pop();
                }
                else
                {
                    PushNode(childNode);
                }
            }
        }

        public void AttachVisitor(IVisitor visitor)
        {
            visitors.Add(visitor);
        }

        private void PushNode(NodeModel node)
        {

            if (!prune.IsPrune(node))
            {
                searchCollection.Push(node);
                foreach (var visitor in visitors)
                {
                    visitor.Visit(node);
                }
            }
        }
    }
}
