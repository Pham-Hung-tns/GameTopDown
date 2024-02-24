using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ProcedualGenarateAlgorithms;

public class CorridorGenerator : SimpleRandomWalk
{
    [SerializeField]
    private int corridorLength = 15, corridorCount = 5;
    [SerializeField]
    [Range(0.1f, 1f)]
    private float roomPercent = 0.8f;
   
    protected override void RunProcedualDungeon()
    {
        CorridorGeneration();
    }

    public void CorridorGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPosition = new HashSet<Vector2Int>();

        List<List<Vector2Int>> corridors = CreateCorridor(floorPositions, potentialRoomPosition);

        for (int i = 0; i < corridors.Count; i++)
        {
            corridors[i] = IncreasingCorridorSizeBy4x4(corridors[i]);
            floorPositions.UnionWith(corridors[i]);
        }

        HashSet<Vector2Int> roomPosition = CreateRooms(potentialRoomPosition);
        // tim tat ca cac ngo cut trong map
        List<Vector2Int> deadEnds = FindAllDeadEnd(floorPositions);
        CreateRoomAtDeadEndPosition(deadEnds, roomPosition);
        floorPositions.UnionWith(roomPosition);
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.GenerateWall(floorPositions, tilemapVisualizer);
    }

    //private List<Vector2Int> IncreasingCorridorSizeByOne(List<Vector2Int> corridor)
    //{
    //    List<Vector2Int> newCorridor = new List<Vector2Int>();
    //    Vector2Int previousDirection = Vector2Int.zero;
    //    for (int i = 1; i< corridor.Count; i++)
    //    {
    //        Vector2Int currentDirection = corridor[i] - corridor[i - 1];
    //        if(previousDirection != Vector2Int.zero && currentDirection != previousDirection)
    //        {
    //            for (int x = -1; x < 2; x++)
    //            {
    //                for (int y = -1; y < 2; y++)
    //                {
    //                    newCorridor.Add(corridor[i-1] + new Vector2Int(x, y));
    //                }
    //            }
    //            previousDirection = currentDirection;
    //        }
    //        else
    //        {
    //            previousDirection = currentDirection;
    //            Vector2Int newCorridorTile = GetDirection90Degree(currentDirection);
    //            newCorridor.Add(corridor[i - 1]);
    //            newCorridor.Add(corridor[i-1] + newCorridorTile);
    //        }
    //    }
    //    return newCorridor;
    //}

    //private Vector2Int GetDirection90Degree(Vector2Int currentDirection)
    //{
    //    if(currentDirection == Vector2Int.up)
    //        return Vector2Int.right;
    //    if (currentDirection == Vector2Int.down)
    //        return Vector2Int.left;
    //    if (currentDirection == Vector2Int.left)
    //        return Vector2Int.up;
    //    if (currentDirection == Vector2Int.right)
    //        return Vector2Int.down;
    //    return Vector2Int.zero;
    //}
    private List<Vector2Int> IncreasingCorridorSizeBy4x4(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        for (int i = 1; i < corridor.Count; i++)
        {
            for (int x = -1; x < 3; x++)
            {
                for (int y = -1; y < 3; y++)
                {
                    newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                }
            }
        }
        return newCorridor;
    }
    private void CreateRoomAtDeadEndPosition(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomPosition)
    {
        foreach (var position in deadEnds)
        {
            // neu roomPosition(vi tri, kich thuoc cua cac room) khong chua position nay => khoi tao deadEnd room.
            if (!roomPosition.Contains(position))
            {
                var roomDeadEnd = RunRandonWalk(dungeonData, position);
                roomPosition.UnionWith(roomDeadEnd);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnd(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighboursPosition = 0;
            foreach (var direction in Direction2D.cardinalDirection)
            {
                if (floorPositions.Contains(position + direction))
                {
                    neighboursPosition++;
                }
            }
            if (neighboursPosition == 1)
            {
                deadEnds.Add(position);
            }
        }
        return deadEnds;
    }

    // lay ra vi tri, kich thuoc cua cac room
    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPosition)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        // so room se duoc tao
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPosition.Count * roomPercent);
        // sap xep cac room theo Guid.NewGuid().
        List<Vector2Int> roomToCreate = potentialRoomPosition.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        // khoi tao room
        foreach (var roomPosition in roomToCreate)
        {
            var roomFloor = RunRandonWalk(dungeonData, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    private List<List<Vector2Int>> CreateCorridor(HashSet<Vector2Int> floorPosition, HashSet<Vector2Int> potentialRoomPosition)
    {
        var currentPosition = startPosition;
        potentialRoomPosition.Add(currentPosition);
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();
        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProcedualGenarateAlgorithms.RandomCorridor(currentPosition, corridorLength);
            corridors.Add(corridor);
            currentPosition = corridor[corridor.Count - 1];
            // lay vi tri cuoi cung cua mot corridor de khoi tao room
            potentialRoomPosition.Add(currentPosition);
            floorPosition.UnionWith(corridor);
        }
        return corridors;
    }
}
