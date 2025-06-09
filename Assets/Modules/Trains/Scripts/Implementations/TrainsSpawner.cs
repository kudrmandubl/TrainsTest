using Modules.Graph.Interfaces;
using Modules.Trains.Configs;
using Modules.Trains.Interfaces;
using Modules.Trains.Views;
using UnityEngine;
using Zenject;

namespace Modules.Trains.Implementations
{
    public class TrainsSpawner : ITrainsSpawner
    {
        private TrainsConfig _config;
        private IGraph _graph;
        private IFactory<ITrain> _trainFactory;

        private TrainsContainer _trainsContainer;

        public ITrain[] Trains { get; private set; }

        public TrainsSpawner(TrainsConfig config,
            IGraph graph,
            IFactory<ITrain> trainFactory,
            TrainsContainer trainsContainer)
        {
            _config = config;
            _graph = graph;
            _trainFactory = trainFactory;
            _trainsContainer = trainsContainer;
        }

        public void SpawnTrains()
        {
            Trains = new ITrain[_config.Trains.Length];
            int id = 0;
            foreach (var trainData in _config.Trains)
            {
                INode startNode = _graph.Nodes[Random.Range(0, _graph.Nodes.Count)];
                Vector3 position = startNode.Position;
                ITrain train = _trainFactory.Create();
                train.UpdateMoveSpeed(trainData.MoveSpeed);
                train.UpdateMiningTimeSeconds(trainData.MiningTimeSeconds);
                train.UpdatePosition(position);

                train.SetCurrentNode(startNode);

                TrainView trainView = GameObject.Instantiate(_config.TrainViewPrefab, position, Quaternion.identity, _trainsContainer.Transform);

                trainView.UpdateMoveSpeed(trainData.MoveSpeed); 
                trainView.MoveSpeed.OnValueChanged += train.UpdateMoveSpeed;
                trainView.UpdateMiningTimeSeconds(trainData.MiningTimeSeconds);
                trainView.MiningTimeSeconds.OnValueChanged += train.UpdateMiningTimeSeconds;

                train.Position.OnValueChanged += trainView.UpdatePosition;
                train.Rotation.OnValueChanged += trainView.UpdateRotation;

                Trains[id] = train;
                id++;
            }
        }
    }
}