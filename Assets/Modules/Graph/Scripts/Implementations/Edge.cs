using Modules.Common;
using Modules.Graph.Interfaces;

namespace Modules.Graph.Implementations
{
    public class Edge : IEdge
    {
        public INode NodeA { get; }
        public INode NodeB { get; }
        public float Distance { get; private set;  }
        public ReactiveProperty<bool> IsSelected { get; }

        public Edge(INode nodeA, INode nodeB, float distance)
        {
            NodeA = nodeA;
            NodeB = nodeB;
            Distance = distance;
            IsSelected = new ReactiveProperty<bool>();
        }

        public void UpdateDistance(float distance)
        {
            Distance = distance;
        }

        public INode GetNeighbor(INode sourceNode)
        {
            return NodeA == sourceNode ? NodeB : NodeB == sourceNode ? NodeA : null;
        }
    }
}