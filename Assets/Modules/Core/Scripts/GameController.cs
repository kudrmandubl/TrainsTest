using Zenject;
using System.Collections.Generic;

public class GameController
{
    private IGraphSpawner _graphSpawner;

    public GameController(IGraphSpawner graphSpawner)
    {
        _graphSpawner = graphSpawner;
        graphSpawner.SpawnGraph();
    }
}