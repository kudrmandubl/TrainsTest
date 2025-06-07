using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Modules.Common.Interfaces;
using Modules.Graph.Data;
using Modules.Graph.Interfaces;
using Modules.Trains.Interfaces;
using UnityEngine;

namespace Modules.Trains.Implementations
{
    public class TrainsLogic : ITrainsLogic
    {
        private const float MineralsUnloadingDuration = 0f;
        private const double MineralsFromMine = 1;

        private IMonoBehaviourCycle _monoBehaviourCycle;
        private ITrainsSpawner _trainsSpawner;
        private IGraph _graph;

        private RouteSelectionData _tempRouteData;

        public TrainsLogic(IMonoBehaviourCycle monoBehaviourCycle, ITrainsSpawner trainsSpawner, IGraph graph)
        {
            _monoBehaviourCycle = monoBehaviourCycle;
            _trainsSpawner = trainsSpawner;
            _graph = graph;

            _tempRouteData = new RouteSelectionData();

            monoBehaviourCycle.SubscribeToUpdate(MoveTrains);
        }

        public void StartMoving()
        {
            foreach (var train in _trainsSpawner.Trains)
            {
                StartMoving(train);
                return;
            }
        }

        private void StartMoving(ITrain train)
        {
            var route = GetNextRoute(train);
            if (route == null)
            {
                Debug.LogError($"�� ������ ������� ��� ������ {JsonUtility.ToJson(train)}");
                return;
            }

            train.AssignRoute(route);
            train.SetNextNode(train.GetNextNode());

            TrainMovement(train);
        }

        private void MoveToNextNode(ITrain train)
        {
            train.MoveToNextEdge();
            train.SetNextNode(train.GetNextNode());

            TrainMovement(train);
        }

        private void TrainMovement(ITrain train)
        {
            float duration = train.GetCurrentEdge().Length / train.MoveSpeed.Value;
            DOTween.To(() => train.CurrentNode.Position, x => train.Position.Value = x, train.NextNode.Position, duration)
                .SetEase(Ease.Linear)
                .OnComplete(() => MoveToNextNode(train));
        }

        private IEnumerable<IEdge> GetNextRoute(ITrain train)
        {
            NodeType targetNodeType = train.Minerals.Value > 0 ? NodeType.Base : NodeType.Mine;
            if (!_graph.TypedNodes.TryGetValue(targetNodeType, out var possibleNodes))
            {
                Debug.LogError($"��� ������ ��������� ����� ��� ���� {targetNodeType}");
                return null;
            }

            _tempRouteData.ProfitCoeff = 0;

            foreach (var node in possibleNodes)
            {
                // TODO: ��������� ������� ����� ������������ - �������� �� ���? 
                var route = _graph.GetRoute(train.CurrentNode, node);

                float moveDuration = 0;
                foreach (var edge in route)
                {
                    moveDuration += edge.Length / train.MoveSpeed.Value;
                }

                float nodeProcessDuration = GetNodeDuration(node, train); 
                double nodeProcessResult = GetNodeResult(node, train);
                
                float profitCoeff = (float) nodeProcessResult / (moveDuration + nodeProcessDuration);
                
                if(_tempRouteData.ProfitCoeff < profitCoeff)
                {
                    _tempRouteData.TargetNode = node;
                    _tempRouteData.Route = route;
                    _tempRouteData.MoveDuration = moveDuration;
                    _tempRouteData.NodeProcessDuration = nodeProcessDuration;
                    _tempRouteData.NodeProcessResult = nodeProcessResult;
                    _tempRouteData.ProfitCoeff = profitCoeff;
                }
            }

            return _tempRouteData.Route;
        }

        private float GetNodeDuration(INode node, ITrain train)
        {
            if(node.Type == NodeType.Mine)
            {
                return train.MiningTimeSeconds.Value * node.Multiplier;
            }
            else if (node.Type == NodeType.Base)
            {
                return MineralsUnloadingDuration;
            }

            Debug.LogError($"�� �������� �������� ����� ��������� ��� ���� {JsonUtility.ToJson(node)}");
            return 0;
        }

        private double GetNodeResult(INode node, ITrain train)
        {
            if (node.Type == NodeType.Mine)
            {
                return MineralsFromMine;
            }
            else if (node.Type == NodeType.Base)
            {
                return train.Minerals.Value * node.Multiplier;
            }

            Debug.LogError($"�� �������� �������� ����� ��������� ��� ���� {JsonUtility.ToJson(node)}");
            return 0;
        }

        private void MoveTrains(float deltaTime)
        {

        }
    }
}