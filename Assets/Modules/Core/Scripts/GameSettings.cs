using Modules.Graph.Configs;
using Modules.Minerals.Configs;
using Modules.Trains.Configs;
using UnityEngine;
using Zenject;

namespace Modules.Core
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Installers/GameSettings")]
    public class GameSettings : ScriptableObjectInstaller
    {
        [SerializeField] private GraphConfig _graphConfig;
        [SerializeField] private TrainsConfig _trainsConfig;
        [SerializeField] private MineralsConfig _mineralsConfig;

        public override void InstallBindings()
        {
            Container.BindInstances(_graphConfig);
            Container.BindInstances(_trainsConfig);
            Container.BindInstances(_mineralsConfig);
        }
    }
}