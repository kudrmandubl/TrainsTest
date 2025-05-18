
using System.Collections.Generic;
using System.Linq;

public class Train : ITrain
{
    private List<IEdge> _route;
    private int _currentSegmentIndex;

    public void AssignRoute(IEnumerable<IEdge> route)
    {
        _route = new List<IEdge>(route);
        _currentSegmentIndex = 0;
    }

    public void Start()
    {
        if (_route == null || !_route.Any())
        {
            UnityEngine.Debug.LogError("������� �� ��������");
            return;
        }
        // �������� �������� �� ��������
        MoveAlongRoute();
    }

    public void Stop()
    {
        // ��������� ������
        UnityEngine.Debug.Log("����� ����������");
    }

    private void MoveAlongRoute()
    {
        // ����� ������ �������� �� ��������
        for (int i = _currentSegmentIndex; i < _route.Count; i++)
        {
            var segment = _route[i];
            // �������� ��������, ��������, ����� �������� ��� ��������
            UnityEngine.Debug.Log($"������ �� ��������: {segment.NodeA.Id} -> {segment.NodeB.Id}");
            _currentSegmentIndex = i + 1;
        }
        UnityEngine.Debug.Log("������� ��������");
    }
}