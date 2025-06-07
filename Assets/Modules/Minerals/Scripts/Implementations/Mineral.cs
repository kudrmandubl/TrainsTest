using Modules.Common;
using Modules.Minerals.Interfaces;

namespace Modules.Minerals.Implementations
{
    public class Mineral : IMineral
    {
        public ReactiveProperty<double> Amount { get; private set; }

        public Mineral()
        {
            Amount = new ReactiveProperty<double>();
        }

        public Mineral(double initialValue = 0)
        {
            Amount.Value = initialValue;
        }

        public void Add(double amount)
        {
            Amount.Value += amount;
        }
    }
}