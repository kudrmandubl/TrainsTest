using Modules.Graph.Implementations;
using Modules.Graph.Interfaces;
using Modules.Graph.Views;
using UnityEngine;
using Zenject;

namespace Modules.Graph.DI
{
    public class GraphInstaller : MonoInstaller
    {
        [SerializeField] private NodesContainer _nodesContainer;
        [SerializeField] private EdgesContainer _edgesContainer;

        public override void InstallBindings()
        {
            Container.Bind<IGraph>().To<Graph.Implementations.Graph>().AsSingle();

            Container.BindInstance<NodesContainer>(_nodesContainer).AsSingle();
            Container.BindInstance<EdgesContainer>(_edgesContainer).AsSingle();

            Container.Bind<IGraphSpawner>().To<GraphSpawner>().AsSingle();
        }

    }
}