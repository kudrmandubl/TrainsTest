using System.Xml.Linq;
using UnityEngine;

public static class GameObjectExtensions
{
    public static T CreateInstance<T>() where T : Component
    {
        var go = new GameObject(typeof(T).ToString());
        return go.AddComponent<T>();
    }
}
