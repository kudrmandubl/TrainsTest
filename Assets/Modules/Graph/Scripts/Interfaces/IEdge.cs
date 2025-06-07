
namespace Modules.Graph.Interfaces
{
    public interface IEdge
    {
        INode NodeA { get; }
        INode NodeB { get; }
        float Distance { get; }

        void UpdateDistance(float legnth);
        INode GetNeighbor(INode sourceNode);
    }
}