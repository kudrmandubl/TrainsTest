using System.Collections.Generic;
using UnityEngine;

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
        foreach (var node in _config.Nodes)
        {
            Vector3 position = new Vector3(node.Position.x, 0f, node.Position.y);
            NodeView nodeView = GameObject.Instantiate(_config.NodeViewPrefabs[node.NodeType], position, Quaternion.identity, _nodesContainer.Transform);
            nodes.Add(nodeView);
        }

        foreach (var edge in _config.Edges)
        {
            Vector3 position = (_config.Nodes[edge.NodeIdA].Position + _config.Nodes[edge.NodeIdB].Position) * 0.5f;
            position.z = position.y;
            position.y = 0f;
            EdgeView edgeView = GameObject.Instantiate(_config.EdgeViewPrefabs, position, Quaternion.identity, _edgesContainer.Transform);

            Vector3 lookAtPosition = _config.Nodes[edge.NodeIdA].Position;
            lookAtPosition.z = lookAtPosition.y;
            lookAtPosition.y = 0f;
            edgeView.Transform.LookAt(lookAtPosition);

            float distance = (_config.Nodes[edge.NodeIdB].Position - _config.Nodes[edge.NodeIdA].Position).magnitude;
            edgeView.SetShape(distance / EdgeView.ModelScale);
        }
    }
}