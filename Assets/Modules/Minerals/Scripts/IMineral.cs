public interface IMineral
{
    float Quantity { get; }
    void Add(float amount);
    void Multiply(float factor);
}