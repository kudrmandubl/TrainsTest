using Modules.UI.Views;
using UnityEngine;
using Zenject;

namespace Modules.UI.DI
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private MainScreen _mainScreen;

        public override void InstallBindings()
        {
            Container.BindInstance<MainScreen>(_mainScreen).AsSingle();
        }

    }
}