using System;
using UnityEngine;

[Serializable]
public class EdgeData
{
    [SerializeField] private int _nodeIdA;
    [SerializeField] private int _nodeIdB;
    [SerializeField] private float _distance;

    public int NodeIdA => _nodeIdA;
    public int NodeIdB => _nodeIdB;
    public float Distance => _distance;
}