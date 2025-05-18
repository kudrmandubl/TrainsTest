
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
            UnityEngine.Debug.LogError("Маршрут не назначен");
            return;
        }
        // Начинаем движение по маршруту
        MoveAlongRoute();
    }

    public void Stop()
    {
        // Остановка поезда
        UnityEngine.Debug.Log("Поезд остановлен");
    }

    private void MoveAlongRoute()
    {
        // Здесь логика движения по маршруту
        for (int i = _currentSegmentIndex; i < _route.Count; i++)
        {
            var segment = _route[i];
            // Имитация движения, например, через задержку или анимацию
            UnityEngine.Debug.Log($"Проход по сегменту: {segment.NodeA.Id} -> {segment.NodeB.Id}");
            _currentSegmentIndex = i + 1;
        }
        UnityEngine.Debug.Log("Маршрут завершен");
    }
}