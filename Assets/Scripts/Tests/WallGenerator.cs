using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ProcedualGenarateAlgorithms;

public class WallGenerator : MonoBehaviour
{
   public static void GenerateWall(HashSet<Vector2Int> positions, TilemapVisualizer tilemapVisualizer)
    {
        var basicWallPositions = FindWallPositionInDungeon(positions, Direction2D.cardinalDirection);
        foreach (var position in basicWallPositions)
        {
            tilemapVisualizer.PaintSingleWall(position);
        }
    }

    private static HashSet<Vector2Int> FindWallPositionInDungeon(HashSet<Vector2Int> floorPositions, List<Vector2Int> cardinalDirections)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions)
        {
            foreach (var direction in cardinalDirections)
            {
                Vector2Int newPosition = position + direction;
                if (!floorPositions.Contains(newPosition))
                {
                    wallPositions.Add(newPosition);
                }
            }
        }
        return wallPositions;
    }
}
