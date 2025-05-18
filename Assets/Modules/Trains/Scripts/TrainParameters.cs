using UnityEngine;

[CreateAssetMenu(fileName = "TrainParameters", menuName = "Config/TrainParameters")]
public class TrainParameters : ScriptableObject
{
    [SerializeField]
    private float _moveSpeed = 200f;
    [SerializeField]
    private float _miningTimeSeconds = 20f;

    public float MoveSpeed => _moveSpeed;
    public float MiningTimeSeconds => _miningTimeSeconds;
}