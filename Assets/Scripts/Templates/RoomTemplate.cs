using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRoomTemplate", menuName ="Dungeon/Room Templates")]
public class RoomTemplate : ScriptableObject
{
    [Header("Textures")]
    public Texture2D[] Textures;

    [Header("Props")]
    public RoomProp[] Props;
}

[Serializable]
public class RoomProp
{
    public string Name;
    public Color PropColor;
    public GameObject PropPrefab;
}
