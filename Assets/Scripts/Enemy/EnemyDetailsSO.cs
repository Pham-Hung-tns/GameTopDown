using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_", menuName = "Scriptable Objects/Enemy/Enemy Details")]
public class EnemyDetailsSO : ScriptableObject
{
    [Header("Prefab & Stats")]
    public GameObject enemyPrefab;
    public float chaseDistance = 5f;

    [Header("Combat")]
    public WeaponDataSO weapon;
    public float firingIntervalMin = 0.5f;
    public float firingIntervalMax = 1.5f;
    public float firingDurationMin = 1f;
    public float firingDurationMax = 2.5f;
    public bool firingLineOfSightRequired = true;

    [Header("Health Per Level")]
    public EnemyHealthDetails[] healthByLevel;
}

[Serializable]
public class EnemyHealthDetails
{
    public DungeonLevelSO dungeonLevel;
    public int health = 10;
}

