using Modules.Common;

namespace Modules.Graph.Interfaces
{
    public interface IEdge
    {
        INode NodeA { get; }
        INode NodeB { get; }
        float Distance { get; }
        ReactiveProperty<bool> IsSelected { get; }

        void UpdateDistance(float distance);
        INode GetNeighbor(INode sourceNode);

    }
}