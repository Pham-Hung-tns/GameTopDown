using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnableObjectRatio<T>
{
    [Tooltip("ScriptableObject or prefab to spawn")]
    public T dungeonObject;

    [Tooltip("Weight used for random selection")]
    public int ratio = 1;
}

[System.Serializable]
public class SpawnableObjectsByLevel<T>
{
    [Tooltip("Dungeon level this configuration applies to")]
    public DungeonLevelSO dungeonLevel;

    [Tooltip("List of spawnable objects with weight ratio")]
    public List<SpawnableObjectRatio<T>> spawnableObjectRatioList = new List<SpawnableObjectRatio<T>>();
}

