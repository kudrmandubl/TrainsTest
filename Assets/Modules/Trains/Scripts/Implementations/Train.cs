using System;
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
        private int _currentSegmentIndex;

        public ReactiveProperty<float> MoveSpeed { get; }
        public ReactiveProperty<float> MiningTimeSeconds { get; }
        public ReactiveProperty<Vector3> Position { get; }
        public ReactiveProperty<Vector3> Rotation { get; }
        public INode CurrentNode { get; private set; }
        public INode NextNode { get; private set; }
        public IMineral Minerals { get; set; }
        public ReactiveProperty<List<IEdge>> Route { get; }

        public Action<ITrain> OnParamChange { get; set; }

        public Train(IMineral mineral)
        {
            MoveSpeed = new ReactiveProperty<float>();
            MiningTimeSeconds = new ReactiveProperty<float>();
            Position = new ReactiveProperty<Vector3>();
            Rotation = new ReactiveProperty<Vector3>();
            Minerals = mineral;
            Route = new ReactiveProperty<List<IEdge>>();
        }

        public void UpdateMoveSpeed(float moveSpeed)
        {
            MoveSpeed.Value = moveSpeed;
            OnParamChange?.Invoke(this);
        }

        public void UpdateMiningTimeSeconds(float miningTimeSeconds)
        {
            MiningTimeSeconds.Value = miningTimeSeconds;
            OnParamChange?.Invoke(this);
        }

        public void UpdatePosition(Vector3 position)
        {
            Position.Value = position;
        }

        public void UpdateRotation(Vector3 rotation)
        {
            Rotation.Value = rotation;
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
            // TODO: убрать создание листа
            Route.Value = new List<IEdge>(route);
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
            return Route.Value[_currentSegmentIndex];
        }

        public bool CheckRouteFinished()
        {
            return _currentSegmentIndex >= Route.Value.Count;
        }
    }
}