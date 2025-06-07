using System.Collections.Generic;
using System.Linq;
using Modules.Graph.Interfaces;

namespace Modules.Graph.Implementations
{
    public class RouteCalculator : IGraphNavigator
    {
        private readonly IGraph _graph;

        public RouteCalculator(IGraph graph)
        {
            _graph = graph;
        }

        public IEnumerable<IEdge> FindOptimalRoute(INode start, INode target)
        {
            // Реализация алгоритма Дейкстры для поиска кратчайшего пути
            var distances = new Dictionary<INode, float>();
            var previousNodes = new Dictionary<INode, IEdge>();
            var unvisited = new HashSet<INode>();

            foreach (var node in _graph.Nodes)
            {
                distances[node] = float.MaxValue;
                unvisited.Add(node);
            }

            distances[start] = 0;

            while (unvisited.Count > 0)
            {
                // Выбираем узел с минимальной дистанцией
                var current = unvisited.OrderBy(n => distances[n]).First();

                if (current == target)
                {
                    break;
                }

                unvisited.Remove(current);

                foreach (var path in _graph.GetEdgesFrom(current))
                {
                    var neighbor = path.NodeB;
                    if (!unvisited.Contains(neighbor))
                    {
                        continue;
                    }

                    float tentativeDistance = distances[current] + path.Length;

                    if (tentativeDistance < distances[neighbor])
                    {
                        distances[neighbor] = tentativeDistance;
                        previousNodes[neighbor] = path;
                    }
                }
            }

            // Восстановление пути
            var route = new List<IEdge>();
            var currentNode = target;

            while (previousNodes.ContainsKey(currentNode))
            {
                var path = previousNodes[currentNode];
                route.Insert(0, path);
                currentNode = path.NodeA;
            }

            return route;
        }
    }
}