using Modules.Trains.Data;
using Modules.Trains.Views;
using UnityEngine;

namespace Modules.Trains.Configs
{
    [CreateAssetMenu(fileName = "TrainsConfig", menuName = "Config/TrainsConfig")]
    public class TrainsConfig : ScriptableObject
    {
        [SerializeField] private TrainData[] _trains;
        [SerializeField] private TrainView _trainViewPrefab;

        public TrainData[] Trains => _trains;
        public TrainView TrainViewPrefab => _trainViewPrefab;

    }
}