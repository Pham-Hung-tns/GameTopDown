using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Door", menuName = "Dungeon/Door Templates")]

public class DungeonLibrary : ScriptableObject
{
    [Header("Levels")]
    public Level[] levels;

    [Header("Door")]
    public GameObject doorNS;
    public GameObject doorWE;
    public GameObject chest;

    [Header("Bonus")]
    public GameObject[] bonus;
    public float range;

    [Header("Portal")]
    public GameObject portal;
}

[Serializable]
public class Level
{
    public string levelName;
    public GameObject[] dungeons;
    public ChestItem itemsInThisLevel;
    public BossStates Boss;
    public NormalEnemyStates[] enemies;

    public int minEnemiesPerRoom;
    public int maxEnemiesPerRoom;

    public int maxBonus;
    public int minBonus;
}
