

namespace Modules.Minerals
{
    public interface IMineralManager
    {
        IMineral Minerals { get; }
        void AddMinerals(double amount);
        void ApplyMultiplier(double factor);
    }
}