using System;
using UnityEngine;

[Serializable]
public class NodeViewData
{
    [SerializeField] private NodeType _nodeType;
    [SerializeField] private NodeView _nodeViewPreafab;

    public NodeType NodeType => _nodeType;
    public NodeView NodeViewPrefab => _nodeViewPreafab;
}