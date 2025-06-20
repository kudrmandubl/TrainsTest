﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        private float _waiter;
        private Dictionary<ITrain, Tween> _trainCurrentActionTweens;

        public TrainsLogic(ITrainsSpawner trainsSpawner, 
            IGraph graph,
            IMineralManager mineralManager)
        {
            _trainsSpawner = trainsSpawner;
            _graph = graph;
            _mineralManager = mineralManager;

            _tempRouteData = new RouteSelectionData();
            _trainCurrentActionTweens = new Dictionary<ITrain, Tween>();
        }

        public void StartMoving()
        {
            foreach (var train in _trainsSpawner.Trains)
            {
                StartMoving(train);
            }
        }

        public void SubscribeToChangeParams()
        {
            foreach (var train in _trainsSpawner.Trains)
            {
                 train.OnParamChange += RestartMoving;
            }

            // поменять на общий интерфейс
            foreach (var node in _graph.Nodes)
            {
                node.OnParamChange += RestartAllTrainsMoving;
            }

            foreach (var edge in _graph.Edges)
            {
                edge.OnParamChange += RestartAllTrainsMoving;
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
                ProcessNode(train, false);
                return;
            }

            train.SetNextNode(train.GetNextNode());

            TrainMovement(train, false, MoveToNextNode);
        }

        private void MoveToNextNode(ITrain train)
        {
            train.MoveToNextEdge();
            if (train.CheckRouteFinished()) 
            {
                ProcessNode(train, false);
                return;
            }

            train.SetNextNode(train.GetNextNode());

            TrainMovement(train, false, MoveToNextNode);
        }

        private void TrainMovement(ITrain train, bool needContinue, Action<ITrain> onCompleteAction)
        {
            // процент пройденного пути
            float pathUncompletedPercent = 1;
            if (needContinue)
            {
                pathUncompletedPercent = (train.NextNode.Position - train.Position.Value).magnitude / (train.NextNode.Position - train.CurrentNode.Position).magnitude;
            }

            float duration = train.GetCurrentEdge().Distance / train.MoveSpeed.Value * pathUncompletedPercent;
            var currentActionTween = DOTween.To(() => train.Position.Value, x => train.UpdatePosition(x), train.NextNode.Position, duration)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    onCompleteAction?.Invoke(train);
                });
            _trainCurrentActionTweens[train] = currentActionTween;

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

        private void ProcessNode(ITrain train, bool needContinue)
        {
            float duration = GetNodeDuration(train.CurrentNode, train);
            if (!needContinue)
            {
                _waiter = 0;
            }

            var currentActionTween = DOTween.To(() => _waiter, x => _waiter = x, 1, duration)
                .OnComplete(() => FinishNodeProcess(train));
            _trainCurrentActionTweens[train] = currentActionTween;
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

        private void RestartMoving(ITrain train)
        {
            _trainCurrentActionTweens[train].Kill();

            // определить обрабатывается ли нода
            if (train.CurrentNode == train.NextNode)
            {
                ProcessNode(train, true);
            }
            else
            {
                // предполагаем, что поезд не может развернуться посреди ребра 
                // поэтому поезд доезжает до конца ребра и после рассчитывает, что делать дальше
                TrainMovement(train, true, FinishEdgeAndStartMoving);
            }
        }

        private void RestartAllTrainsMoving()
        {
            foreach (var train in _trainsSpawner.Trains)
            {
                RestartMoving(train);
            }
        }

        private void FinishEdgeAndStartMoving(ITrain train)
        {
            train.MoveToNextEdge();
            StartMoving(train);
        }
    }
}