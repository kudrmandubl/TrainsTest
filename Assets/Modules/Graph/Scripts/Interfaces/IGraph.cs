using System.Collections.Generic;
using Modules.Graph.Data;

namespace Modules.Graph.Interfaces
{
    public interface IGraph
    {
        List<INode> Nodes { get; }
        Dictionary<NodeType, List<INode>> TypedNodes { get; }
        List<IEdge> Edges { get; }
        void AddNode(INode node);
        void AddEdge(IEdge edge);
        IEnumerable<IEdge> GetEdgesFrom(INode node);
        IEnumerable<IEdge> GetRoute(INode startNode, INode endNode);
    }
}