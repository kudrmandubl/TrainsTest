using Modules.Minerals.Configs;
using Modules.Minerals.Interfaces;
using Modules.Minerals.Views;
using Modules.UI.Views;
using UnityEngine;

namespace Modules.Minerals.Implementations
{
    public class MineralManager : IMineralManager
    {

        private MineralsConfig _config;
        private MainScreen _mainScreen;

        public IMineral Minerals { get; private set; }

        public MineralManager(MineralsConfig config,
            IMineral mineral,
            MainScreen mainScreen)
        {
            _config = config;
            Minerals = mineral;
            _mainScreen = mainScreen;
        }

        public void SpawnUI()
        {
            MineralsUIView mineralUIView = GameObject.Instantiate(_config.MineralsUIViewPrefab, _mainScreen.Transform);
            Minerals.Amount.OnValueChanged += mineralUIView.UpdateText;
        }

        public void AddMinerals(double amount)
        {
            Minerals.Add(amount);
        }
    }
}