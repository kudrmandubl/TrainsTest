using Modules.Graph.Interfaces;
using Modules.Trains.Interfaces;

namespace Modules.Core
{
    public class GameController
    {

        public GameController(IGraphSpawner graphSpawner,
            ITrainsSpawner trainsSpawner,
            ITrainsLogic trainsLogic)
        {
            graphSpawner.SpawnGraph();
            trainsSpawner.SpawnTrains();
            trainsLogic.StartMoving();
        }
    }
}