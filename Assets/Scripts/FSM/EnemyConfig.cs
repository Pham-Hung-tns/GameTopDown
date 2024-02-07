using UnityEngine;

[CreateAssetMenu]
public class EnemyConfig : ScriptableObject
{
    [Header("Idle State")]
    public float timeToWander;

    [Header("Wander State")]
    public float timeToIdle;
    public float speed;
    public LayerMask obstacleLayer;
    public Vector3 moveRange;

    [Header("Chase State")]
    public float timeToAttack;

    [Header("Attack State")]
    public float timeBetweenAttack;
}

