using System.Collections.Generic;
using Modules.Common;
using Modules.Graph.Interfaces;
using Modules.Minerals.Interfaces;
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
        public INode CurrentNode { get; private set; }
        public INode NextNode { get; private set; }
        public IMineral Minerals { get; set; }

        public Train(IMineral mineral)
        {
            MoveSpeed = new ReactiveProperty<float>();
            MiningTimeSeconds = new ReactiveProperty<float>();
            Position = new ReactiveProperty<Vector3>();
            Minerals = mineral;
        }

        public void UpdateMoveSpeed(float moveSpeed)
        {
            MoveSpeed.Value = moveSpeed;
        }

        public void UpdateMiningTimeSeconds(float miningTimeSeconds)
        {
            MiningTimeSeconds.Value = miningTimeSeconds;
        }

        public void UpdatePosition(Vector3 position)
        {
            Position.Value = position;
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
            SetCurrentNode(NextNode);
            _currentSegmentIndex++;
        }

        public IEdge GetCurrentEdge()
        {
            return _route[_currentSegmentIndex];
        }

        public bool CheckRouteFinished()
        {
            return _currentSegmentIndex >= _route.Count;
        }
    }
}