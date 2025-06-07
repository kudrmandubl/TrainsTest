using Modules.Common;

namespace Modules.Minerals.Interfaces
{
    public interface IMineral
    {
        ReactiveProperty<double> Amount { get; }
        void Add(double amount);
    }
}