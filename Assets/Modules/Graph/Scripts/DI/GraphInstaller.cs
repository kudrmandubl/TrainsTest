using UnityEngine;
using Zenject;

public class GraphInstaller : MonoInstaller
{
    [SerializeField] private NodesContainer _nodesContainer;
    [SerializeField] private EdgesContainer _edgesContainer;

    public override void InstallBindings()
    {
        GraphView graphView = GameObjectExtensions.CreateInstance<GraphView>();
        Container.BindInstance<GraphView>(graphView).AsSingle();

        Container.Bind<IGraph>().To<Graph>().AsSingle();

        Container.BindInstance<NodesContainer>(_nodesContainer).AsSingle();
        Container.BindInstance<EdgesContainer>(_edgesContainer).AsSingle();

        Container.Bind<IGraphSpawner>().To<GraphSpawner>().AsSingle();
    }

}