﻿using System;
using Modules.Graph.Data;
using Modules.Graph.Interfaces;
using UnityEngine;

namespace Modules.Graph.Implementations
{
    public class Node : INode
    {
        public int Id { get; }
        public NodeType Type { get; }
        public float Multiplier { get; private set; }
        public Vector3 Position { get; }

        public Action OnParamChange { get; set; }

        public Node(int id,
            NodeType type,
            float multiplier,
            Vector3 position)
        {
            Id = id;
            Type = type;
            Multiplier = multiplier;
            Position = position;
        }

        public void UpdateMultiplier(float multiplier)
        {
            Multiplier = multiplier;
            OnParamChange?.Invoke();
        }
    }
}