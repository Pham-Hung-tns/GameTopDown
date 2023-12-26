using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class ItemData : ScriptableObject
{
    [Header("Config")]
    public string Name;
    public Sprite icon;

    public virtual void Pickup()
    {

    }
}
