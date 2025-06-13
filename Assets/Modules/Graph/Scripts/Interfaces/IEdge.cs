using System;
using Modules.Common;

namespace Modules.Graph.Interfaces
{
    public interface IEdge
    {
        INode NodeA { get; }
        INode NodeB { get; }
        float Distance { get; }
        ReactiveProperty<bool> IsSelected { get; }

        Action OnParamChange { get; set; }

        void UpdateDistance(float distance);
        INode GetNeighbor(INode sourceNode);

    }
}