using System;
using UnityEngine;

namespace Modules.Trains.Data
{
    [Serializable]
    public class TrainData
    {
        [SerializeField] private float _moveSpeed = 200f;
        [SerializeField] private float _miningTimeSeconds = 20f;

        public float MoveSpeed => _moveSpeed;
        public float MiningTimeSeconds => _miningTimeSeconds;
    }
}