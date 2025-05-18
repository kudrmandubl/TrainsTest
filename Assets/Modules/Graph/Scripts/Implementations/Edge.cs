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
}