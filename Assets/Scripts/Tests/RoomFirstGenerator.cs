using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomFirstGenerator : SimpleRandomWalk
{
    [SerializeField] private int minRoomWidth, minRoomHeigh;
    [SerializeField] private int dungeonWidth, dungeonHeigh;
    [SerializeField]
    [Range(0, 10)]
    private int offset; // lam cho room nho lai, hoac lon hon 
    [SerializeField]
    private bool randomWalkingGeneratorRoom;

    protected override void RunProcedualDungeon()
    {
        CreateRoom();
    }

    private void CreateRoom()
    {
        // lay vi tri cua cac room sau khi duoc splitttt
        var roomlist = ProcedualGenarateAlgorithms.BinarySpacePartitining(new BoundsInt((Vector3Int)startPosition, 
            new Vector3Int(dungeonWidth, dungeonHeigh,0)), minRoomWidth, minRoomHeigh);
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        if (randomWalkingGeneratorRoom)
        {
            floor = CreateRandomWalkRoom(roomlist);
        }
        else
        {
            floor = CreateSimpleRoom(roomlist);
        }
        // lay ra vi tri trung tam cua cac room
        List<Vector2Int> centerPosition = new List<Vector2Int>();
        foreach (var room in roomlist)
        {
            centerPosition.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = ConnectRooms(centerPosition);
        floor.UnionWith(corridors);
        // ve dungeon
        tilemapVisualizer.PaintFloorTiles(floor);
        WallGenerator.GenerateWall(floor, tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreateRandomWalkRoom(List<BoundsInt> roomlist)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomlist)
        {
            var roomBound = room;
            var centerPosition = new Vector2Int(Mathf.RoundToInt(room.center.x), Mathf.RoundToInt(room.center.y));
            var roomFloor = RunRandonWalk(dungeonData, centerPosition);
            // neu vi tri nao con nam trong kich thuoc cua room, add no vao floor
            foreach (var position in roomFloor)
            {
                if (position.x > (roomBound.xMin + offset) && position.x < (roomBound.xMax - offset)
                    && position.y > (roomBound.yMin + offset) && position.y < (roomBound.yMax - offset))
                {
                    floor.Add(position);
                }

            }
        }
        return floor;
    }
    private HashSet<Vector2Int> CreateSimpleRoom(List<BoundsInt> roomGenerator)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomGenerator)
        {
            for (int col = 0; col < room.size.x - offset; col++)
            {
                for (int row = 0; row < room.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int)(room.min + new Vector3Int(col, row));
                    floor.Add(position);
                }
            }
        }
        return floor;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> centerPositionOfRooms)
    {
        // di tu vi tri trung tam cua room nay den room gan nhat

        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentCenterRoom = centerPositionOfRooms[UnityEngine.Random.Range(0, centerPositionOfRooms.Count)];
        centerPositionOfRooms.Remove(currentCenterRoom);

        while(centerPositionOfRooms.Count > 0)
        {
            // tim ra room gan voi room hien tai nhat
            Vector2Int closest = FindRoomClosest(currentCenterRoom, centerPositionOfRooms);
            centerPositionOfRooms.Remove(closest);
            // tao corridor
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentCenterRoom, closest);
            currentCenterRoom = closest;
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentCenterRoom, Vector2Int destination)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var position = currentCenterRoom;
        corridor.Add(position);
        while(position.y != destination.y)
        {
            if(destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            else if(destination.y > position.y) 
            {
                position += Vector2Int.up;
            }
            corridor.Add(position);
        }
        while(position.x != destination.x)
        {
            if(destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            corridor.Add(position);
        }
        corridor = IncreasingCorridorSizeBy2x2(corridor);
        return  new HashSet<Vector2Int>(corridor);
    }
    private List<Vector2Int> IncreasingCorridorSizeBy2x2(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        for (int i = 1; i < corridor.Count; i++)
        {
            for (int x = -1; x < 1; x++)
            {
                for (int y = -1; y < 1; y++)
                {
                    newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                }
            }
        }
        return newCorridor;
    }

    private Vector2Int FindRoomClosest(Vector2Int currentCenterRoom, List<Vector2Int> centerPosition)
    {
        Vector2Int closest = Vector2Int.zero;
        float length = float.MaxValue;
        foreach (var position in centerPosition)
        {
            if(length > Vector2Int.Distance(position, currentCenterRoom))
            {
                length = Vector2Int.Distance(position,currentCenterRoom);
                closest = position;
            }
        }
       return closest;
    }

}
