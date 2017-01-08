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
    class WideTree : IThreadTree<NodeModel> 
    {
        private ISearchCollection<NodeModel> searchCollection;
        private IPrune prune;
        private IList<IVisitor> visitors;
        private INodeProcessor<NodeModel> nodeProcessor;
        private IThreadPool threadPool;

        public WideTree(ISearchCollection<NodeModel> searchMode, INodeProcessor<NodeModel> nodeProcessor, IPrune prune, IThreadPool threadPool)
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
                var currentNodeModel = searchCollection.Pop();
             
                while (this.nodeProcessor.HasMoreChildren(currentNodeModel))
                {
                    var childNode = this.nodeProcessor.GenerateNextChild(currentNodeModel);
                    if (childNode != null && !childNode.Equals(NodeProcessor.VidNode))
                    {
                        if (prune.IsPrune(childNode))
                        {
                            continue;
                        }

                        // Only push the node if it does have children
                        if (this.nodeProcessor.HasMoreChildren(childNode))
                        {
                            var shouldPushNode = true;
                            if(masterThread || this.MasterThread){
                                shouldPushNode = !this.threadPool.TryNewThreadSearch(childNode);
                            }

                            if (shouldPushNode)
                            {
                                searchCollection.Push(childNode);
                            }
                        }

                        foreach (var visitor in visitors)
                        {
                            visitor.Visit(childNode);
                        }
                    }
                }
            }
            if (masterThread || this.MasterThread)
            {
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

        public bool MasterThread
        {
            get;
            set;
        }
    }
}
