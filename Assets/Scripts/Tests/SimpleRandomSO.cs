using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DataDungeon", menuName ="Test Dungeon/ ProcedualDungeonSO")]
public class SimpleRandomSO : ScriptableObject
{
    public int iterations;
    public int walkLength = 10;
    public bool startRandomlyEachIteration;
}
