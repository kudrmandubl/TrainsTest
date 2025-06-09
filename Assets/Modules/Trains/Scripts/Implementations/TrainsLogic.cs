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
        private const float RotationDuration = 0.2f;

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
                Debug.LogError($"Не найден маршрут для поезда {JsonUtility.ToJson(train)}");
                return;
            }

            train.AssignRoute(route);
            // на случай, если поезд заспаунился на точке, в которою ему выгоднее всего ехать
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
            float duration = train.GetCurrentEdge().Distance / train.MoveSpeed.Value;
            DOTween.To(() => train.CurrentNode.Position, x => train.UpdatePosition(x), train.NextNode.Position, duration)
                .SetEase(Ease.Linear)
                .OnComplete(() => MoveToNextNode(train));

            float rotationDuration = duration < RotationDuration ? duration : RotationDuration;
            Vector3 targetRotation = train.Rotation.Value;
            targetRotation.y = GetLookRotationY(train.CurrentNode.Position, train.NextNode.Position);
            DOTween.To(() => train.Rotation.Value, x => train.UpdateRotation(x), targetRotation, rotationDuration);
        }

        private IEnumerable<IEdge> GetNextRoute(ITrain train)
        {
            NodeType targetNodeType = train.Minerals.Amount.Value > 0 ? NodeType.Base : NodeType.Mine;
            if (!_graph.TypedNodes.TryGetValue(targetNodeType, out var possibleNodes))
            {
                Debug.LogError($"Нет набора возможных нодов для типа {targetNodeType}");
                return null;
            }

            _tempRouteData.ProfitCoeff = 0;

            foreach (var node in possibleNodes)
            {
                // TODO: постоянно создают новые перечисления - заменить на пул? 
                var route = _graph.GetRoute(train.CurrentNode, node);

                float moveDuration = 0;
                foreach (var edge in route)
                {
                    moveDuration += edge.Distance / train.MoveSpeed.Value;
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

            Debug.LogError($"Не возможно получить время обработки для ноды {JsonUtility.ToJson(node)}");
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
                return train.Minerals.Amount.Value * node.Multiplier;
            }

            Debug.LogError($"Не возможно получить время обработки для ноды {JsonUtility.ToJson(node)}");
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
                train.Minerals.Add(-train.Minerals.Amount.Value);
                _mineralManager.AddMinerals(nodeResult);
            }

            StartMoving(train);
        }

        private float GetLookRotationY(Vector3 characterPosition, Vector3 targetPosition)
        {
            // Создаем направление на цель, игнорируя вертикаль
            Vector3 direction = targetPosition - characterPosition;
            direction.y = 0; // Ограничиваем вращение только по оси Y

            if (direction.sqrMagnitude == 0)
            {
                // Если персонаж уже смотрит прямо на цель или цель совпадает с позицией
                return 0f;
            }

            // Рассчитываем угол в градусах
            float angleY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            return angleY;
        }
    }
}