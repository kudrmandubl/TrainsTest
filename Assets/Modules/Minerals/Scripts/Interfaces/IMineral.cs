
namespace Modules.Minerals.Interfaces
{
    public interface IMineral
    {
        double Value { get; }
        void Add(double amount);
    }
}