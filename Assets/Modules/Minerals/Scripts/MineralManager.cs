using UnityEngine;

public class MineralManager : IMineralManager
{
    public IMineral Minerals { get; private set; }

    public MineralManager()
    {
        Minerals = new Mineral();
    }

    public void AddMinerals(float amount)
    {
        Minerals.Add(amount);
    }

    public void ApplyMultiplier(float factor)
    {
        Minerals.Multiply(factor);
    }
}