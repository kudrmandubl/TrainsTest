using System.Collections.Generic;
using Modules.Graph.Data;
using Modules.Graph.Views;
using UnityEngine;

namespace Modules.Graph.Configs
{
    [CreateAssetMenu(fileName = "GraphConfig", menuName = "Config/GraphConfig")]
    public class GraphConfig : ScriptableObject
    {
        [SerializeField] private NodeData[] _nodes;
        [SerializeField] private EdgeData[] _edges;
        [SerializeField] private NodeViewData[] _nodeViews;
        [SerializeField] private EdgeView _edgeViewPrefabs;

        [SerializeField] private Material _selectedEdgeMaterial;
        [SerializeField] private Material _unselectedEdgeMaterial;

        private Dictionary<NodeType, NodeView> _nodeViewPrefabs;

        public NodeData[] Nodes => _nodes;
        public EdgeData[] Edges => _edges;
        public EdgeView EdgeViewPrefabs => _edgeViewPrefabs;
        public Material SelectedEdgeMaterial => _selectedEdgeMaterial;
        public Material UnselectedEdgeMaterial => _unselectedEdgeMaterial;
        public Dictionary<NodeType, NodeView> NodeViewPrefabs
        {
            get
            {
                if (_nodeViewPrefabs == null)
                {
                    _nodeViewPrefabs = new Dictionary<NodeType, NodeView>();
                    foreach (var nodeView in _nodeViews)
                    {
                        _nodeViewPrefabs.Add(nodeView.NodeType, nodeView.NodeViewPrefab);
                    }
                }
                return _nodeViewPrefabs;
            }
        }
    }
}