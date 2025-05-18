using System.Collections.Generic;
using System.Linq;

public class Graph : IGraph
{
    private readonly List<INode> _nodes;
    private readonly List<IEdge> _edges;

    public List<INode> Nodes => _nodes;
    public List<IEdge> Edge => _edges;

    public Graph()
    {
        _nodes = new List<INode>();
        _edges = new List<IEdge>();
    }

    public void AddNode(INode node)
    {
        _nodes.Add(node);
    }

    public void AddEdge(IEdge path)
    {
        _edges.Add(path);
    }

    public IEnumerable<IEdge> GetEdgesFrom(INode node)
    {
        return _edges.Where(p => p.NodeA == node);
    }
}