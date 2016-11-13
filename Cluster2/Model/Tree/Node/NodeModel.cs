using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2.Model.Tree.Node
{
    class NodeModel
    {
        public NodeModel(){
            Clusters = new LinkedList<int[]>();
        }

        public LinkedListNode<int[]> currentToBeMerged { get; set; }
        public LinkedListNode<int[]> toBeMergedWith { get; set; }

        public LinkedListNode<int[]> Arrow { get; set; }
        
        public LinkedList<int[]> Clusters;

        public int Bullet { get; set; }

        public double SumOfSquares { get; set; }

        public override string ToString()
        {
            var strBuilder = new StringBuilder();
            var iterator = this.Clusters.First;
            var outerSeparator = "";
            while (iterator != null)
            {
                strBuilder.Append(string.Format("{0}{1}", outerSeparator, "{"));
                var innerSeparator = "";
                foreach (var item in iterator.Value)
                {
                    strBuilder.Append(string.Format("{0}{1}", innerSeparator, item));
                    innerSeparator = ",";
                }
                strBuilder.Append("}");
                outerSeparator = ",";
                iterator = iterator.Next;
            }
            strBuilder.Append(" => " + this.SumOfSquares);
            strBuilder.Append("\n");
            return strBuilder.ToString();
        }
    }
}
