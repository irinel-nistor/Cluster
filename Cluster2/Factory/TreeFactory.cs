using Cluster2.Model.Points;
using Cluster2.Model.Search;
using Cluster2.Model.Tree;
using Cluster2.Model.Tree.Node;
using Cluster2.Model.Tree.Prune;
using Cluster2.Model.Visitor;
using Cluster2.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cluster2.Factory
{
    class TreeFactory
    {
        private IPrune prune;
        private SumOfSquareCalculator sumCalculator;
        private IList<IVisitor> visitors;
        private Dictionary<int, Point> coordinates;
        private NodeProcessor nodeProcessor;
        private ThreadCount threadCount;
        private ISearchCollection<NodeModel> searchCollection;

        public TreeFactory(Dictionary<int, Point> coordinates)
        {
            this.visitors = new List<IVisitor>();
            this.coordinates = coordinates;

            var space = new Space(coordinates);
            this.sumCalculator = new SumOfSquareCalculator(space);
            this.prune = new Prune(space.PointMapping.Count);
            this.nodeProcessor = new NodeProcessor(coordinates.Keys, sumCalculator);
            this.threadCount = new ThreadCount();
            this.searchCollection = new DfsSearch<NodeModel>();
        }

        public ITree<NodeModel> create()
        {
            var threadPool = new ThreadsPool(this, this.threadCount);
            var tree = new WideTree(new DfsSearch<NodeModel>(), nodeProcessor, prune, threadPool);
            foreach(var visitor in visitors){
                tree.AttachVisitor(visitor);
            }

            return tree;
        }

        public IThreadTree<NodeModel> create(NodeModel rootNode, Action<Thread> onThreadClose)
        {
            var searchCollection = new ExtendedPrioritySearch<NodeModel>(new SumNodeComparer<NodeModel>());
            searchCollection.Push(rootNode);

            var threadPool = new ThreadsPool(this, this.threadCount);
            var tree = new WideTree(searchCollection, nodeProcessor, prune, threadPool);
            var threadTree = new ThreadTree(tree, onThreadClose);

            foreach (var visitor in visitors)
            {
                threadTree.AttachVisitor(visitor);
            }

            return threadTree;
        }

        private ISearchCollection<NodeModel> GetSearchCollection(){
            var auxSearchCollection = this.searchCollection;
            this.searchCollection = new ExtendedPrioritySearch<NodeModel>(new SumNodeComparer<NodeModel>());

            return auxSearchCollection;
        }

        public void AttachVisitor(IVisitor visitor)
        {
            visitors.Add(visitor);
        }

        public override string ToString(){
            return this.prune.ToString();
        }
    }
}
