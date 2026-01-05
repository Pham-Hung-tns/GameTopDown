using UnityEngine;

[CreateAssetMenu]
public class EnemyConfig : ScriptableObject
{
    [Header("Vitality")]
    public int Health;

    [Header("Detection")]
    public LayerMask obstacleLayer;
    public LayerMask playerLayer;

    public float rangeCanDetectPlayer;

    [Header("Idle State")]
    public float timeToWander;

    [Header("Wander State")]
    public float timeToIdle;
    public float speed;
    public Vector3 moveRange;

    [Header("Chase State")]
    public float timeToAttack;

    [Header("Attack State")]
    public float timeBetweenAttack;

    [Header("Weapon")]
    public Weapon initialWeapon;
}

