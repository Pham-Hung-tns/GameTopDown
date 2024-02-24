using System.Collections.Generic;
using UnityEngine;

public static class ProcedualGenarateAlgorithms
{
    //tao mot dungeon moi bang cach di chuyen theo cac huong khac nhau (luu cac vi tri vao HashSet) toi da walkLength buoc.
    public static HashSet<Vector2Int> SimpleWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(startPosition);
        var previourPosition = startPosition;
        for (int i = 0; i < walkLength; i++)
        {
            var newPositon = previourPosition + Direction2D.GetRandomCardinalDirection();
            previourPosition = newPositon;
            path.Add(newPositon);
        }

        return path;
    }
    public static List<Vector2Int> RandomCorridor(Vector2Int starPosition, int corridorLength)
    {
        List<Vector2Int> corridors = new List<Vector2Int>();
        var currentPosition = starPosition;
        var direction = Direction2D.GetRandomCardinalDirection();
        corridors.Add(starPosition);
        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridors.Add(currentPosition);
        }
        return corridors;
    }
    public static List<BoundsInt> BinarySpacePartitining(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomList = new List<BoundsInt>();
        roomQueue.Enqueue(spaceToSplit);
        while (roomQueue.Count > 0)
        {
            var room = roomQueue.Dequeue();
            if (room.size.x > minWidth && room.size.y > minHeight)
            {
                if (Random.value < 0.3f)
                {
                    if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minWidth, minHeight, roomQueue, room);
                    }
                    else if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, minHeight, roomQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.y > minHeight)
                    {
                        roomList.Add(room);
                    }
                }
                else
                {
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, minHeight, roomQueue, room);
                    }
                    else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minWidth, minHeight, roomQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.y > minHeight)
                    {
                        roomList.Add(room);
                    }
                }
            }
        }
        return roomList;
    }

    private static void SplitVertically(int minWidth, int minHeight, Queue<BoundsInt> roomQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z), new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomQueue.Enqueue(room1);
        roomQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minWidth, int minHeight, Queue<BoundsInt> roomQueue, BoundsInt room)
    {
        var ySplit = UnityEngine.Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z), new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
        roomQueue.Enqueue(room1);
        roomQueue.Enqueue(room2);
    }

    public static class Direction2D
    {
        public static List<Vector2Int> cardinalDirection = new List<Vector2Int>
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right,
        };

        public static Vector2Int GetRandomCardinalDirection()
        {
            return cardinalDirection[UnityEngine.Random.Range(0, cardinalDirection.Count)];
        }
    }
}
