using Modules.Graph.Interfaces;

namespace Modules.Graph.Implementations
{
    public class Edge : IEdge
    {
        public INode NodeA { get; }
        public INode NodeB { get; }
        public float Length { get; }

        public Edge(INode nodeA, INode nodeB, float length)
        {
            NodeA = nodeA;
            NodeB = nodeB;
            Length = length;
        }

        public INode GetNeighbor(INode sourceNode)
        {
            return NodeA == sourceNode ? NodeB : NodeB == sourceNode ? NodeA : null;
        }
    }
}