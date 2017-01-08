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
        private IThreadPool threadPool;

        public Tree(ISearchCollection<NodeModel> searchMode, INodeProcessor<NodeModel> nodeProcessor, IPrune prune, IThreadPool threadPool)
        {
            this.searchCollection = searchMode;
            this.visitors = new List<IVisitor>();
            this.prune = prune;
            this.nodeProcessor = nodeProcessor;
            this.threadPool = threadPool;
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
                        threadSearch(currenNodeModel);
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

        private void threadSearch(NodeModel currentNodeModel){
            while (this.threadPool.HasAvailableThreads() && this.nodeProcessor.HasMoreChildren(currentNodeModel))
            {
                var childNode = this.nodeProcessor.GenerateNextChild(currentNodeModel);
                if (childNode != null && !childNode.Equals(NodeProcessor.VidNode))
                {
                    if (!prune.IsPrune(childNode))
                    {
                        // Only push the node if it does have children
                        if (this.nodeProcessor.HasMoreChildren(childNode))
                        {
                            this.threadPool.TryNewThreadSearch(childNode);
                        }

                        foreach (var visitor in visitors)
                        {
                            visitor.Visit(childNode);
                        }
                    }    
                }
            }
            
            if (!this.nodeProcessor.HasMoreChildren(currentNodeModel))
            {
                searchCollection.Pop();
            }        
        }
    }
}
