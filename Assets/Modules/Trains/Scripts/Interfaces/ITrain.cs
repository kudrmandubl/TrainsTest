using System;
using System.Collections.Generic;
using Modules.Common;
using Modules.Graph.Interfaces;
using Modules.Minerals.Interfaces;
using UnityEngine;

namespace Modules.Trains.Interfaces
{
    public interface ITrain
    {
        ReactiveProperty<float> MoveSpeed { get; }
        ReactiveProperty<float> MiningTimeSeconds { get; }
        ReactiveProperty<Vector3> Position { get; }
        ReactiveProperty<Vector3> Rotation { get; }
        INode CurrentNode { get; }
        INode NextNode { get; }
        IMineral Minerals { get; set; }
        ReactiveProperty<List<IEdge>> Route { get; }

        Action<ITrain> OnParamChange { get; set; }

        void UpdateMoveSpeed(float moveSpeed);
        void UpdateMiningTimeSeconds(float miningTimeSeconds);
        void UpdatePosition(Vector3 position);
        void UpdateRotation(Vector3 rotation);
        void SetCurrentNode(INode currentNode);
        void SetNextNode(INode nextNode);

        void AssignRoute(IEnumerable<IEdge> route);
        INode GetNextNode();
        void MoveToNextEdge();
        IEdge GetCurrentEdge();
        bool CheckRouteFinished();
    }
}