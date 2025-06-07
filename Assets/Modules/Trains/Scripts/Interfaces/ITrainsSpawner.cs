
namespace Modules.Trains.Interfaces
{
    public interface ITrainsSpawner
    {
        ITrain[] Trains { get; }

        void SpawnTrains();
    }
}