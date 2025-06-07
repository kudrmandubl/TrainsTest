
namespace Modules.Minerals
{
    public interface IMineral
    {
        double Value { get; }
        void Add(double amount);
        void Multiply(double factor);
    }
}