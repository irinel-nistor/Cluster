using Cluster2.Model.Points;
using Cluster2.Model.Tree;
using Cluster2.Model.Tree.Node;
using Cluster2.Model.Tree.Prune;
using Cluster2.Model.Visitor;
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
        private ISearchCollection<NodeModel> searchCollection;
        private IPrune prune;
        private SumOfSquareCalculator sumCalculator;
        ThreadsPool threadPool;
        private IList<IVisitor> visitors;
        private Dictionary<int, Point> coordinates;
        private NodeProcessor nodeProcessor;

        public TreeFactory(ISearchCollection<NodeModel> searchCollection, Dictionary<int, Point> coordinates)
        {
            this.searchCollection = searchCollection;
            this.threadPool = new ThreadsPool(this);
            this.visitors = new List<IVisitor>();
            this.coordinates = coordinates;

            var space = new Space(coordinates);
            this.sumCalculator = new SumOfSquareCalculator(space);
            this.prune = new Prune(space.PointMapping.Count);
            this.nodeProcessor = new NodeProcessor(coordinates.Keys, sumCalculator);
        }

        public ITree<NodeModel> create()
        {
            var tree = new Tree(this.searchCollection, nodeProcessor, prune, threadPool);
            foreach(var visitor in visitors){
                tree.AttachVisitor(visitor);
            }

            return tree;
        }

        public ITree<NodeModel> create(ISearchCollection<NodeModel> searchCollection, Action<Thread> onThreadClose)
        {
            var tree = new Tree(searchCollection, nodeProcessor, prune, threadPool);
            var threadTree = new ThreadTree(tree, onThreadClose);

            foreach (var visitor in visitors)
            {
                threadTree.AttachVisitor(visitor);
            }

            return threadTree;
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
