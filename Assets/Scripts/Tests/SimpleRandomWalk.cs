using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleRandomWalk : AbstractDungeonGenerator
{
    [SerializeField] protected SimpleRandomSO dungeonData;

    protected override void RunProcedualDungeon()
    {
        HashSet<Vector2Int> floorPositions = RunRandonWalk(dungeonData, startPosition);
        tilemapVisualizer.ClearTilemap();
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.GenerateWall(floorPositions, tilemapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandonWalk(SimpleRandomSO simpleRandomSO, Vector2Int position)
    {
        var currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < simpleRandomSO.iterations; i++)
        {
            var path = ProcedualGenarateAlgorithms.SimpleWalk(currentPosition, simpleRandomSO.walkLength);
            floorPositions.UnionWith(path);
            if (simpleRandomSO.startRandomlyEachIteration)
            {
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }
        return floorPositions;
    }
}
