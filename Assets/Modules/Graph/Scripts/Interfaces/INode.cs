using Modules.Graph.Data;
using UnityEngine;

namespace Modules.Graph.Interfaces
{
    /// <summary>
    /// ��������� ������� �����
    /// </summary>
    public interface INode
    {
        int Id { get; }
        NodeType Type { get; }
        float Multiplier { get; }
        Vector3 Position { get; }

        void UpdateMultiplier(float multiplier);
    }
}