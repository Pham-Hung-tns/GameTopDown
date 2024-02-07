using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class GameData
{
    public int totalCoin;
    public GameData()
    {
        totalCoin = 10;
    }
}
