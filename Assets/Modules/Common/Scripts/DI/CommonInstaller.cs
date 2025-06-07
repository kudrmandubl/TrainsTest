using Modules.Common.Implementations;
using Modules.Common.Interfaces;
using Zenject;

namespace Modules.Common.DI
{
    public class CommonInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IMonoBehaviourCycle>()
                .To<SimpleMonoBehaviourCycle>()
                .FromNewComponentOnNewGameObject()
                .AsSingle();
        }
    }
}