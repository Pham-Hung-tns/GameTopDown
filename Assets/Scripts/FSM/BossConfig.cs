using System.Threading;
using UnityEngine;

[CreateAssetMenu]
public class BossConfig : ScriptableObject
{
    [Header("Idle State")]
    public float timeToAttack;

    [Header("Attack Random State")]
    public float amountOfProjectiles;
    public float timeBtwAttack;
    public float minSpeed;
    public float maxSpeed;
    public float minSpawnTime;
    public float maxSpawnTime;
}

