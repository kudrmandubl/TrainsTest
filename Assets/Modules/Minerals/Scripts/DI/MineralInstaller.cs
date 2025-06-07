using Modules.Minerals.Implementations;
using Modules.Minerals.Interfaces;
using Zenject;

namespace Modules.Minerals.DI
{
    public class MineralInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IMineral>().To<Mineral>().AsTransient();
            Container.Bind<IMineralManager>().To<MineralManager>().AsSingle();
        }
    }
}
