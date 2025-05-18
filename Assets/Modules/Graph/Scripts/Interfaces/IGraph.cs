using System.Collections.Generic;

public interface IGraph
{
    IEnumerable<INode> Nodes { get; }
    IEnumerable<IEdge> Edge { get; }
    IEnumerable<IEdge> GetEdgesFrom(INode node);
}
