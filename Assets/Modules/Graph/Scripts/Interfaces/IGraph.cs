using System.Collections.Generic;

public interface IGraph
{
    List<INode> Nodes { get; }
    List<IEdge> Edge { get; }
    void AddNode(INode node);
    void AddEdge(IEdge edge);
    IEnumerable<IEdge> GetEdgesFrom(INode node);
}
