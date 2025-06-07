using System;
using UnityEngine;

namespace Modules.Graph.Data
{
    [Serializable]
    public class NodeData
    {
        [SerializeField] private NodeType _nodeType;
        [SerializeField] private float _multiplier;
        [SerializeField] private Vector2 _position;

        public NodeType NodeType => _nodeType;
        public float Multiplier => _multiplier;
        public Vector2 Position => _position;
    }
}