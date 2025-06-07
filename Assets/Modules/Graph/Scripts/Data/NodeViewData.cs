using System;
using Modules.Graph.Views;
using UnityEngine;

namespace Modules.Graph.Data
{
    [Serializable]
    public class NodeViewData
    {
        [SerializeField] private NodeType _nodeType;
        [SerializeField] private NodeView _nodeViewPrefab;

        public NodeType NodeType => _nodeType;
        public NodeView NodeViewPrefab => _nodeViewPrefab;
    }
}