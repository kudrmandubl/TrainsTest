using System;
using Modules.Graph.Data;
using UnityEngine;

namespace Modules.Graph.Interfaces
{
    /// <summary>
    /// Èםעונפויס גונרטםû דנאפא
    /// </summary>
    public interface INode
    {
        int Id { get; }
        NodeType Type { get; }
        float Multiplier { get; }
        Vector3 Position { get; }

        Action OnParamChange { get; set; }

        void UpdateMultiplier(float multiplier);
    }
}