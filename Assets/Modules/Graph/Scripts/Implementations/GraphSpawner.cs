using System.Collections.Generic;
using Modules.Graph.Configs;
using Modules.Graph.Interfaces;
using Modules.Graph.Views;
using UnityEngine;

namespace Modules.Graph.Implementations
{
    public class GraphSpawner : IGraphSpawner
    {
        private GraphConfig _config;
        private IGraph _graph;

        private NodesContainer _nodesContainer;
        private EdgesContainer _edgesContainer;

        public GraphSpawner(GraphConfig config,
            IGraph graph,
            NodesContainer nodesContainer,
            EdgesContainer edgesContainer)
        {
            _config = config;
            _graph = graph;
            _nodesContainer = nodesContainer;
            _edgesContainer = edgesContainer;
        }

        public void SpawnGraph()
        {
            List<NodeView> nodes = new List<NodeView>();
            nodes.Capacity = _config.Nodes.Length;
            _graph.Nodes.Capacity = nodes.Capacity;
            int id = 0;
            foreach (var nodeData in _config.Nodes)
            {
                Vector3 position = new Vector3(nodeData.Position.x, 0f, nodeData.Position.y);

                INode node = new Node(id, nodeData.NodeType, nodeData.Multiplier, position);
                _graph.AddNode(node);

                NodeView nodeView = GameObject.Instantiate(_config.NodeViewPrefabs[nodeData.NodeType], position, Quaternion.identity, _nodesContainer.Transform);
                nodes.Add(nodeView);

                nodeView.UpdateMultiplier(nodeData.Multiplier);
                // подписываемся на изменения во view, т.к. нужна возможность через инспектор менять значения для расчётов
                nodeView.Multiplier.OnValueChanged += node.UpdateMultiplier;

                id++;
            }

            _graph.Edge.Capacity = _config.Edges.Length;
            foreach (var edgeData in _config.Edges)
            {
                Vector3 position = (_config.Nodes[edgeData.NodeIdA].Position + _config.Nodes[edgeData.NodeIdB].Position) * 0.5f;
                position.z = position.y;
                position.y = 0f;
                EdgeView edgeView = GameObject.Instantiate(_config.EdgeViewPrefabs, position, Quaternion.identity, _edgesContainer.Transform);

                Vector3 lookAtPosition = _config.Nodes[edgeData.NodeIdA].Position;
                lookAtPosition.z = lookAtPosition.y;
                lookAtPosition.y = 0f;
                edgeView.Transform.LookAt(lookAtPosition);

                float distance = (_config.Nodes[edgeData.NodeIdB].Position - _config.Nodes[edgeData.NodeIdA].Position).magnitude;
                edgeView.SetShape(distance / EdgeView.ModelScale);

                IEdge edge = new Edge(_graph.Nodes[edgeData.NodeIdA], _graph.Nodes[edgeData.NodeIdB], edgeData.Distance);
                _graph.AddEdge(edge);

                edgeView.UpdateDistance(edgeData.Distance);
                // подписываемся на изменения во view, т.к. нужна возможность через инспектор менять значения для расчётов
                edgeView.Distance.OnValueChanged += edge.UpdateDistance;

                edge.IsSelected.OnValueChanged += (isSelected) => UpdateEdgeSelectedMaterial(edgeView, isSelected);
            }
        }

        private void UpdateEdgeSelectedMaterial(EdgeView edgeView, bool isSelected)
        {
            edgeView.UpdateSelectedMaterial(isSelected ? _config.SelectedEdgeMaterial : _config.UnselectedEdgeMaterial);
        }
    }
}