using System;
using System.Collections.Generic;
using Modules.Common;
using Modules.Graph.Interfaces;
using UnityEngine;

namespace Modules.Trains.Views
{
    public class TrainView : CashedMonoBehaviour
    {
        // колдовство для сохранения разделения данных и отображения
        // и при этом выполнения условий задания про возможность через инспектор поменять значения 
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _miningTimeSeconds;

        public ReactiveProperty<float> MoveSpeed { get; } = new ReactiveProperty<float>();
        public ReactiveProperty<float> MiningTimeSeconds { get; } = new ReactiveProperty<float>();
        public List<IEdge> Route { get; private set; }

        public Action<IEnumerable<IEdge>> OnRouteChange;

        public void UpdateMoveSpeed(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
            MoveSpeed.Value = moveSpeed;
        }

        public void UpdateMiningTimeSeconds(float miningTimeSeconds)
        {
            _miningTimeSeconds = miningTimeSeconds;
            MiningTimeSeconds.Value = miningTimeSeconds;
        }

        public void UpdatePosition(Vector3 position)
        {
            Transform.position = position;
        }

        public void UpdateRotation(Vector3 rotation)
        {
            Transform.localEulerAngles = rotation;
        }

        public void UpdateRoute(List<IEdge> route)
        {
            Route = route;
            OnRouteChange?.Invoke(route);
        }

        // При изменениях в инспекторе (только в редакторе)
        private void OnValidate()
        {
            // Обновляем значение в ReactiveProperty, если оно отличается
            if (MoveSpeed.Value != _moveSpeed)
            {
                MoveSpeed.Value = _moveSpeed;
            }
            if (MiningTimeSeconds.Value != _miningTimeSeconds)
            {
                MiningTimeSeconds.Value = _miningTimeSeconds;
            }
        }
    }
}