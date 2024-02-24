using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField] protected Vector2Int startPosition;
    [SerializeField] protected TilemapVisualizer tilemapVisualizer = null;

    public void GenerateDungeon()
    {
        tilemapVisualizer.ClearTilemap();
        RunProcedualDungeon();
    }

    protected virtual void RunProcedualDungeon()
    {
        
    }
}
