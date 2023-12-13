using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Stat", menuName = "Weapon/ new Weapon")]
public class Weapon : ScriptableObject
{
    [Header("Information")]
    public string Name;
    public Sprite icon;

    [Header("Stat")]
    public int damage;
    public int energy;
    public int minSpread;
    public int maxSpread;
}
