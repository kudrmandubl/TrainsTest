
namespace Modules.Minerals
{
    public class Mineral : IMineral
    {
        public double Value { get; private set; }

        public Mineral(double initialValue = 0)
        {
            Value = initialValue;
        }

        public void Add(double amount)
        {
            Value += amount;
        }

        public void Multiply(double factor)
        {
            Value *= factor;
        }
    }
}