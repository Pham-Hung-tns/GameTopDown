
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsInTheLevel", menuName = "Dungeon/ Chest Item")]
public class ChestItem : ScriptableObject
{
    public PickableItem[] AvalibleItems;
}

