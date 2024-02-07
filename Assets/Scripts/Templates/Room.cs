using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public enum RoomType
{
    RoomEntrance,
    RoomEmpty,
    RoomBoss,
    RoomEnemy

}
public class Room : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private bool useDebug;
    [SerializeField] private RoomType roomType;

    [Header("Grid")]
    [SerializeField] private Tilemap extraTilemap;

    [Header("Door Transform")]
    [SerializeField] private Transform[] posDoorNS;
    [SerializeField] private Transform[] posDoorWE;



    private Dictionary<Vector3, bool> tiles = new Dictionary<Vector3, bool>();
    private List<Door> doorsOfRoom;

    public static event Action<Room> OnPlayerEnterTheRoom;
    public bool roomCompleted { get; set; }

    public RoomType RoomType => roomType;


    private void Start()
    {
        doorsOfRoom = new List<Door>();
        GetTile();
        GenerateRoomUsingTemplate();
        GenerateDoor();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!NormalRoom() && roomType != RoomType.RoomBoss)
            return;


        if (collision.CompareTag("Player"))
        {
            if(OnPlayerEnterTheRoom != null)
            {
                OnPlayerEnterTheRoom?.Invoke(this);
            }
        }
    }

    public void OpenRoom()
    {
        foreach (Door door in doorsOfRoom)
        {
            door.HideDoor();
        }
    }
    public void SetRoomCompleted()
    {
        roomCompleted = true;
        OpenRoom();
    }
    public void CloseRoom()
    {
        foreach (Door door in doorsOfRoom)
        {
            door.ShowDoor();
        }
    }

    private void GenerateDoor()
    {
        if(posDoorNS.Length > 0)
        {
            for(int i = 0; i < posDoorNS.Length; i++)
            {
                GameObject doorN_GO = Instantiate(LevelManager.Instance.DoorSO.doorNS, posDoorNS[i]);
                Door door = doorN_GO.GetComponentInChildren<Door>();
                door.HideDoor();
                doorsOfRoom.Add(door);
            }
        }

        if (posDoorWE.Length > 0)
        {
            for (int i = 0; i < posDoorWE.Length; i++)
            {
                GameObject doorE_GO = Instantiate(LevelManager.Instance.DoorSO.doorWE, posDoorWE[i]);
                Door door = doorE_GO.GetComponentInChildren<Door>();
                door.HideDoor();
                doorsOfRoom.Add(door);
            }
        }
    }

    private void GetTile()
    {
        if(NormalRoom())
        {
            foreach(Vector3Int posTile in extraTilemap.cellBounds.allPositionsWithin)
            {
                Vector3 position = extraTilemap.CellToWorld(posTile);
                Vector3 newPos = new Vector3(position.x, position.y,position.z);
                tiles.Add(newPos, true);
            }
        }
        else
            return;
    }

    private void GenerateRoomUsingTemplate()
    {
        if(!NormalRoom() || roomType == RoomType.RoomBoss)
        {
            return;
        }
        //chon template ngau nhien
        int randomTemplate = UnityEngine.Random.Range(0, LevelManager.Instance.RoomTemplates.Textures.Length);
        Texture2D texture = LevelManager.Instance.RoomTemplates.Textures[randomTemplate];

        // danh sach cac position duoc danh dau tu truoc
        List<Vector3> positions = new List<Vector3>(tiles.Keys);
        for(int y = 0, a =0;y < texture.height; y++)
        {
            for(int x = 0; x < texture.width; x++, a++)
            {
                // lay ra mau cua tung pixel trong template mau
                Color pixelColor = texture.GetPixel(x,y);

                // so sanh voi danh sach mau co san => tao Prop theo mau
                foreach(RoomProp prop in LevelManager.Instance.RoomTemplates.Props)
                {
                    if(pixelColor == prop.PropColor)
                    {
                        GameObject propInstance = Instantiate(prop.PropPrefab, extraTilemap.transform);
                        propInstance.transform.position = new Vector3(positions[a].x, positions[a].y, 0f);

                        if (tiles.ContainsKey(positions[a]))
                        {
                            tiles[positions[a]] = false;
                        }
                    }
                }
            }
        }
    }

    public Vector3 GetTilePosition()
    {
        List<Vector3> availibleTiles = (from tile in tiles
                                        where tile.Value == true
                                        select tile.Key).ToList();
        int randomTileIndex = UnityEngine.Random.Range(0, availibleTiles.Count);
        Vector3 pos = availibleTiles[randomTileIndex];
        return pos;
    }
    public Vector3 PositionOfBoss()
    {
        List<Vector3> availibleTiles = (from tile in tiles
                                        where tile.Value
                                        select tile.Key).ToList();
        return availibleTiles[(int)availibleTiles.Count /2];
    }

    private bool NormalRoom()
    {
        return roomType == RoomType.RoomEnemy;
    }

    private void OnDrawGizmosSelected()
    {
        if(useDebug)
        {
            if(tiles.Count > 0)
            {
                foreach(KeyValuePair<Vector3, bool> tile in tiles)
                {
                    if(tile.Value)
                    {
                        Gizmos.color = Color.blue;
                        Gizmos.DrawWireCube(tile.Key, Vector2.one * 0.8f);
                    }
                    else
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawSphere(tile.Key, 0.1f);
                    }
                }
            }
        }
    }

}
