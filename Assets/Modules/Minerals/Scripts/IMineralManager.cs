public interface IMineralManager
{
    IMineral Minerals { get; }
    void AddMinerals(float amount);
    void ApplyMultiplier(float factor);
}
