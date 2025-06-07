using System.Linq;
using Modules.Graph.Interfaces;
using Modules.Trains.Configs;
using Modules.Trains.Interfaces;
using Modules.Trains.Views;
using UnityEngine;

namespace Modules.Trains.Implementations
{
    public class TrainsSpawner : ITrainsSpawner
    {
        private TrainsConfig _config;
        private IGraph _graph;

        private TrainsContainer _trainsContainer;

        public ITrain[] Trains { get; private set; }

        public TrainsSpawner(TrainsConfig config,
            IGraph graph,
            TrainsContainer trainsContainer)
        {
            _config = config;
            _graph = graph;
            _trainsContainer = trainsContainer;
        }

        public void SpawnTrains()
        {
            Trains = new ITrain[_config.Trains.Length];
            int id = 0;
            foreach (var trainData in _config.Trains)
            {
                INode startNode = _graph.Nodes.FirstOrDefault(x => x.Id == 6);//[Random.Range(0, _graph.Nodes.Count)];
                Vector3 position = startNode.Position;
                ITrain train = new Train(trainData.MoveSpeed, trainData.MiningTimeSeconds, position);
                train.SetCurrentNode(startNode);

                TrainView trainView = GameObject.Instantiate(_config.TrainViewPrefab, position, Quaternion.identity, _trainsContainer.Transform);

                trainView.MoveSpeed.OnValueChanged += train.UpdateMoveSpeed;
                trainView.MiningTimeSeconds.OnValueChanged += train.UpdateMiningTimeSeconds;

                train.Position.OnValueChanged += trainView.UpdatePosition;

                Trains[id] = train;
                id++;
            }
        }
    }
}