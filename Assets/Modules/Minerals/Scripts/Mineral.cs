
public class Mineral : IMineral
{
    public float Quantity { get; private set; }

    public Mineral(float initialQuantity = 0)
    {
        Quantity = initialQuantity;
    }

    public void Add(float amount)
    {
        Quantity += amount;
    }

    public void Multiply(float factor)
    {
        Quantity *= factor;
    }
}