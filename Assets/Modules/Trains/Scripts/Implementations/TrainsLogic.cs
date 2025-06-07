using System.Collections.Generic;
using DG.Tweening;
using Modules.Graph.Data;
using Modules.Graph.Interfaces;
using Modules.Minerals.Interfaces;
using Modules.Trains.Interfaces;
using UnityEngine;

namespace Modules.Trains.Implementations
{
    public class TrainsLogic : ITrainsLogic
    {
        private const float MineralsUnloadingDuration = 0f;
        private const double MineralsFromMine = 1;

        private ITrainsSpawner _trainsSpawner;
        private IGraph _graph;
        private IMineralManager _mineralManager;

        private RouteSelectionData _tempRouteData;

        public TrainsLogic(ITrainsSpawner trainsSpawner, 
            IGraph graph,
            IMineralManager mineralManager)
        {
            _trainsSpawner = trainsSpawner;
            _graph = graph;
            _mineralManager = mineralManager;

            _tempRouteData = new RouteSelectionData();
        }

        public void StartMoving()
        {
            foreach (var train in _trainsSpawner.Trains)
            {
                StartMoving(train);
            }
        }

        private void StartMoving(ITrain train)
        {
            var route = GetNextRoute(train);
            if (route == null)
            {
                Debug.LogError($"Ќе найден маршрут дл€ поезда {JsonUtility.ToJson(train)}");
                return;
            }

            train.AssignRoute(route);
            // на случай, если поезд заспаунилс€ на точке, в которою ему выгоднее всего ехать
            if (train.CheckRouteFinished())
            {
                ProcessNode(train);
                return;
            }

            train.SetNextNode(train.GetNextNode());

            TrainMovement(train);
        }

        private void MoveToNextNode(ITrain train)
        {
            train.MoveToNextEdge();
            if (train.CheckRouteFinished()) 
            {
                ProcessNode(train);
                return;
            }

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
                Debug.LogError($"Ќет набора возможных нодов дл€ типа {targetNodeType}");
                return null;
            }

            _tempRouteData.ProfitCoeff = 0;

            foreach (var node in possibleNodes)
            {
                // TODO: посто€нно создают новые перечислени€ - заменить на пул? 
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

            Debug.LogError($"Ќе возможно получить врем€ обработки дл€ ноды {JsonUtility.ToJson(node)}");
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

            Debug.LogError($"Ќе возможно получить врем€ обработки дл€ ноды {JsonUtility.ToJson(node)}");
            return 0;
        }

        private void ProcessNode(ITrain train)
        {
            float duration = GetNodeDuration(train.CurrentNode, train);
            float waiter = 0;
            DOTween.To(() => waiter, x => waiter = x, 1, duration)
                .OnComplete(() => FinishNodeProcess(train));
        }

        private void FinishNodeProcess(ITrain train)
        {
            double nodeResult = GetNodeResult(train.CurrentNode, train);
            
            if (train.CurrentNode.Type == NodeType.Mine)
            {
                train.Minerals.Add(nodeResult);
            }
            else if (train.CurrentNode.Type == NodeType.Base)
            {
                train.Minerals.Add(-train.Minerals.Value);
                _mineralManager.AddMinerals(nodeResult);
            }

            StartMoving(train);
        }
    }
}