using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Installers/GameSettings")]
public class GameSettings : ScriptableObjectInstaller
{
    public GraphConfig GraphConfig;

    public override void InstallBindings()
    {
        Container.BindInstances(GraphConfig);
    }
}