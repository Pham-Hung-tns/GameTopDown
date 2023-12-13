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
}

[Serializable]
public class Level
{
    public string levelName;
    public GameObject[] dungeons;
}
