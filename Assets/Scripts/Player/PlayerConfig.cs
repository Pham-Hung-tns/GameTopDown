using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerConfig : ScriptableObject
{
    [Header("Data")]
    public int Level;
    public string Name;
    public Sprite Icon;

    [Header("Value")]
    public float currentHealth;
    public float MaxHealth;
    public float currentArmor;
    public float MaxArmor;
    public float currentEnergy;
    public float MaxEnergy;
    public float CriticalChance;
    public float CriticalDamage;

    [Header("Extra")]
    public bool unlock;
    public int unlockCost;
    public int upgradeCost;
    public int upgradeCostPercent;

    [Header("Prefab")]
    public GameObject playerPrefab;
}
