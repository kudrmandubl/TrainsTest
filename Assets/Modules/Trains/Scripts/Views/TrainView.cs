using Modules.Common;
using UnityEngine;

namespace Modules.Trains.Views
{
    public class TrainView : CashedMonoBehaviour
    {
        public ReactiveProperty<float> MoveSpeed { get; } = new ReactiveProperty<float>();
        public ReactiveProperty<float> MiningTimeSeconds { get; } = new ReactiveProperty<float>();

        public void UpdatePosition(Vector3 position)
        {
            Transform.position = position;
        }
    }
}