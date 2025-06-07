using Modules.Trains.Implementations;
using Modules.Trains.Interfaces;
using Modules.Trains.Views;
using UnityEngine;
using Zenject;

namespace Modules.Trains.DI
{
    public class TrainInstaller : MonoInstaller
    {
        [SerializeField] private TrainsContainer _trainsContainer;

        public override void InstallBindings()
        {
            Container.BindInstance<TrainsContainer>(_trainsContainer).AsSingle();

            Container.Bind<ITrainsSpawner>().To<TrainsSpawner>().AsSingle();
            Container.Bind<ITrainsLogic>().To<TrainsLogic>().AsSingle();
        }

    }
}