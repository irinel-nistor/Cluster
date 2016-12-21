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
    class Tree : ITree<NodeModel> 
    {
        private ISearchCollection<NodeModel> searchCollection;
        private IPrune prune;
        private IList<IVisitor> visitors;
        private INodeProcessor<NodeModel> nodeProcessor;
        private ThreadsPool threadPool;
        private ClusterCache clusterCache;

        public Tree(ISearchCollection<NodeModel> searchMode, INodeProcessor<NodeModel> nodeProcessor, IPrune prune, ThreadsPool threadPool)
        {
            this.searchCollection = searchMode;
            this.visitors = new List<IVisitor>();
            this.prune = prune;
            this.nodeProcessor = nodeProcessor;
            this.threadPool = threadPool;
            this.clusterCache = clusterCache;
        }

        public void Search()
        {
            var rooNodeModel = this.nodeProcessor.GenerateRoot();
            PushNode(rooNodeModel);
            this.Search(true);
        }

        public void Search(bool masterThread)
        {
            while (searchCollection.Count != 0)
            {
                var currenNodeModel = searchCollection.Peek();
                var childNode = this.nodeProcessor.GenerateNextChild(currenNodeModel);
                if (childNode != null && !childNode.Equals(NodeProcessor.VidNode))
                {
                    PushNode(childNode);      
                }

                // Remove the node if it doesn't have anymore children
                if (!this.nodeProcessor.HasMoreChildren(currenNodeModel))
                {
                    searchCollection.Pop();
                }
                else
                {
                    if(masterThread){
                        this.threadPool.TryNewThreadSearch(currenNodeModel);
                    }
                }
            }

            if (masterThread) {
                this.threadPool.WaitAll();
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
         
                // Only push the node if it does have children
                if (this.nodeProcessor.HasMoreChildren(node))
                {
                    searchCollection.Push(node);
                }

                foreach (var visitor in visitors)
                {
                    visitor.Visit(node);
                }
            }
        }
    }
}
