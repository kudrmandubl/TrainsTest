
/// <summary>
/// ��������� ������� �����
/// </summary>
public interface INode
{
    int Id { get; }
    NodeType Type { get; }
    float MineralMultiplier { get; }
}