using Modules.Minerals.Interfaces;

namespace Modules.Minerals.Implementations
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
    }
}