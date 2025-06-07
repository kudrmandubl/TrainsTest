

namespace Modules.Minerals.Interfaces
{
    public interface IMineralManager
    {
        IMineral Minerals { get; }
        void SpawnUI();
        void AddMinerals(double amount);
    }
}