
namespace Modules.Graph.Interfaces
{
    public interface IEdge
    {
        INode NodeA { get; }
        INode NodeB { get; }
        float Length { get; }

        INode GetNeighbor(INode sourceNode);
    }
}