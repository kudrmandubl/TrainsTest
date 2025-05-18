using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Container.Bind<IGraph>().To<Graph>().AsSingle();
        Container.Bind<IGraphNavigator>().To<RouteCalculator>().AsTransient();
        Container.Bind<IMineralManager>().To<MineralManager>().AsSingle();
        Container.Bind<ITrain>().To<Train>().AsTransient();

        Container.Bind<GameController>().AsSingle().NonLazy();
    }
}