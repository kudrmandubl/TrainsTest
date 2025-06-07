using System.Collections.Generic;
using System.Linq;
using Modules.Common;
using Modules.Graph.Interfaces;
using Modules.Minerals;
using Modules.Trains.Interfaces;
using UnityEngine;

namespace Modules.Trains.Implementations
{
    public class Train : ITrain
    {
        private List<IEdge> _route;
        private int _currentSegmentIndex;

        public ReactiveProperty<float> MoveSpeed { get; }
        public ReactiveProperty<float> MiningTimeSeconds { get; }
        public ReactiveProperty<Vector3> Position { get; }
        public Vector3 Destination { get; private set; }
        public INode CurrentNode { get; private set; }
        public INode NextNode { get; private set; }
        public IMineral Minerals { get; set; }

        public Train(float moveSpeed, float miningTimeSeconds, Vector3 position)
        {
            MoveSpeed = new ReactiveProperty<float>();
            MoveSpeed.Value = moveSpeed;
            MiningTimeSeconds = new ReactiveProperty<float>();
            MiningTimeSeconds.Value = miningTimeSeconds;
            Position = new ReactiveProperty<Vector3>();
            Position.Value = position;
            Minerals = new Mineral();
        }

        public void UpdateMoveSpeed(float moveSpeed)
        {
            MoveSpeed.Value = moveSpeed;
        }

        public void UpdateMiningTimeSeconds(float miningTimeSeconds)
        {
            MiningTimeSeconds.Value = miningTimeSeconds;
        }

        public void SetDestination(Vector3 destination)
        {
            Destination = destination;
        }

        public void SetCurrentNode(INode currentNode)
        {
            CurrentNode = currentNode;
        }

        public void SetNextNode(INode nextNode)
        {
            NextNode = nextNode;
        }

        public void AssignRoute(IEnumerable<IEdge> route)
        {
            _route = new List<IEdge>(route);
            _currentSegmentIndex = 0;
        }

        public INode GetNextNode()
        {
            return GetCurrentEdge().NodeA == CurrentNode ? GetCurrentEdge().NodeB : GetCurrentEdge().NodeA;
        }

        public void MoveToNextEdge()
        {
            CurrentNode = NextNode;
            _currentSegmentIndex++;
        }

        public IEdge GetCurrentEdge()
        {
            return _route[_currentSegmentIndex];
        }

        public void Start()
        {
            if (_route == null || !_route.Any())
            {
                Debug.LogError("Маршрут не назначен");
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
}