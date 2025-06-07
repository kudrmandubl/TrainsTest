using Zenject;

namespace Modules.Core
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameController>().AsSingle().NonLazy();
        }
    }
}