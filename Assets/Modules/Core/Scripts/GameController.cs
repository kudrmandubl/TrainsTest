using Modules.Graph.Interfaces;
using Modules.Minerals.Interfaces;
using Modules.Trains.Interfaces;

namespace Modules.Core
{
    public class GameController
    {

        public GameController(IGraphSpawner graphSpawner,
            ITrainsSpawner trainsSpawner,
            ITrainsLogic trainsLogic,
            IMineralManager mineralManager)
        {
            graphSpawner.SpawnGraph();
            trainsSpawner.SpawnTrains();
            trainsLogic.StartMoving();
            trainsLogic.SubscribeToChangeParams();
            mineralManager.SpawnUI();
        }
    }
}