

namespace Modules.Minerals.Interfaces
{
    public interface IMineralManager
    {
        IMineral Minerals { get; }
        void AddMinerals(double amount);
    }
}