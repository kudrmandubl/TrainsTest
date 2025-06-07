using UnityEngine;

namespace Modules.Minerals
{
    public class MineralManager : IMineralManager
    {
        public IMineral Minerals { get; private set; }

        public MineralManager()
        {
            Minerals = new Mineral();
        }

        public void AddMinerals(double amount)
        {
            Minerals.Add(amount);
        }

        public void ApplyMultiplier(double factor)
        {
            Minerals.Multiply(factor);
        }
    }
}