public interface IEdge
{
    INode NodeA { get; }
    INode NodeB { get; }
    float Length { get; }
}