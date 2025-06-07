using System.Collections.Generic;
using Modules.Common;
using Modules.Graph.Interfaces;
using Modules.Minerals;
using UnityEngine;

namespace Modules.Trains.Interfaces
{
    public interface ITrain
    {
        ReactiveProperty<float> MoveSpeed { get; }
        ReactiveProperty<float> MiningTimeSeconds { get; }
        ReactiveProperty<Vector3> Position { get; }
        Vector3 Destination { get; }
        INode CurrentNode { get; }
        INode NextNode { get; }
        IMineral Minerals { get; set; }

        void UpdateMoveSpeed(float moveSpeed);
        void UpdateMiningTimeSeconds(float miningTimeSeconds);
        void SetDestination(Vector3 destionation);
        void SetCurrentNode(INode currentNode);
        void SetNextNode(INode nextNode);

        void AssignRoute(IEnumerable<IEdge> route);
        INode GetNextNode();
        void MoveToNextEdge();
        IEdge GetCurrentEdge();
        void Start();
        void Stop();
    }
}