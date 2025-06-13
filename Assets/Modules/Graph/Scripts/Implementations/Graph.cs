using System.Collections.Generic;
using System.Linq;
using Modules.Graph.Data;
using Modules.Graph.Interfaces;

namespace Modules.Graph.Implementations
{
    public class Graph : IGraph
    {
        private readonly List<INode> _nodes;
        private readonly Dictionary<NodeType, List<INode>> _typedNodes;
        private readonly List<INode> _baseNodes;
        private readonly List<IEdge> _edges;

        public List<INode> Nodes => _nodes;
        public Dictionary<NodeType, List<INode>> TypedNodes => _typedNodes;
        public List<IEdge> Edges => _edges;

        public Graph()
        {
            _nodes = new List<INode>();
            _typedNodes = new Dictionary<NodeType, List<INode>>()
            {
                { NodeType.Mine, new List<INode>() },
                { NodeType.Base, new List<INode>() },
            };
            _edges = new List<IEdge>();
        }

        public void AddNode(INode node)
        {
            _nodes.Add(node);

            if (_typedNodes.TryGetValue(node.Type, out var typeNodes))
            {
                typeNodes.Add(node);
            }
        }

        public void AddEdge(IEdge path)
        {
            _edges.Add(path);
        }

        public IEnumerable<IEdge> GetEdgesFrom(INode node)
        {
            return _edges.Where(p => p.NodeA == node || p.NodeB == node);
        }

        public IEnumerable<IEdge> GetRoute(INode startNode, INode endNode)
        {
            var distances = new Dictionary<INode, float>();
            var previousNodes = new Dictionary<INode, INode>();
            var unvisited = new HashSet<INode>();

            // Инициализация
            foreach (var node in _nodes)
            {
                distances.Add(node, float.PositiveInfinity);
                previousNodes.Add(node, null);
                unvisited.Add(node);
            }

            distances[startNode] = 0f;

            while (unvisited.Count > 0)
            {
                // Выбираем ноду с минимальной дистанцией
                INode currentNode = unvisited.OrderBy(n => distances[n]).First();

                if (currentNode == endNode)
                {
                    // Найден кратчайший путь
                    break;
                }

                unvisited.Remove(currentNode);

                // Обновляем расстояния для соседей
                var edges = GetEdgesFrom(currentNode);
                foreach (var edge in edges)
                {
                    var neighbor = edge.GetNeighbor(currentNode);

                    if (!unvisited.Contains(neighbor))
                        continue;

                    float tentativeDistance = distances[currentNode] + edge.Distance;
                    if (tentativeDistance < distances[neighbor])
                    {
                        distances[neighbor] = tentativeDistance;
                        previousNodes[neighbor] = currentNode;
                    }
                }
            }

            // Восстановление пути
            var pathEdges = new List<IEdge>();
            var current = endNode;

            while (previousNodes[current] != null)
            {
                var prev = previousNodes[current];

                // Находим ребро между current и prev
                var edge = _edges.FirstOrDefault(e =>
                    (e.NodeA == current && e.NodeB == prev) ||
                    (e.NodeA == prev && e.NodeB == current));

                if (edge != null)
                {
                    pathEdges.Add(edge);
                }
                else
                {
                    break;
                }

                current = prev;
            }

            pathEdges.Reverse(); // порядок от старта к финишу

            // Если путь не найден, вернуть пустой список
            if (distances[endNode] == float.PositiveInfinity)
                return new List<IEdge>();

            return pathEdges;
        }
    }
}