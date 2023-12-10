using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("Player")]
    [SerializeField] private PlayerMove player;

    [Header("Templates")]
    [SerializeField] private RoomTemplate roomTemplates;
    [SerializeField] private DungeonLibrary dungeonLibrary;
    public RoomTemplate RoomTemplates => roomTemplates;
    public DungeonLibrary DoorSO => dungeonLibrary;

    private Room currentRoom;
    private int currentLevelIndex;
    private int currentDungeonIndex;
    private GameObject currentDungeonGO;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        CreateLevel();
    }
    private void CreateLevel()
    {
        currentDungeonGO =  Instantiate(dungeonLibrary.levels[currentLevelIndex].dungeons[currentDungeonIndex], transform);
    }

    private void OnEnable()
    {
        Room.OnPlayerEnterTheRoom += PlayerEnterRoom;
        Portal.OnNextDungeon += PortalEventCallBack;
    }

    private void OnDisable()
    {
        Room.OnPlayerEnterTheRoom -= PlayerEnterRoom;
        Portal.OnNextDungeon -= PortalEventCallBack;
    }

    private void PortalEventCallBack()
    {
        ContinueDungeonNext();  
    }

    public void PositionOfPlayerInDungeon()
    {
        Room[] dungeonRooms = currentDungeonGO.GetComponentsInChildren<Room>();
        Room posDefault = null;
        foreach (Room room in dungeonRooms)
        {
            if(room.RoomType == RoomType.RoomEntrance)
            {
                posDefault = room;
            }
        }

        if(posDefault != null)
        {
            if(player != null)
            {
                player.transform.position = posDefault.transform.position;
            }
        }
    }

    private void ContinueDungeonNext()
    {
        currentDungeonIndex++;
        if (currentDungeonIndex >= dungeonLibrary.levels[currentLevelIndex].dungeons.Length)
        {
            currentLevelIndex++;
            currentDungeonIndex = 0;
        }
        Destroy(currentDungeonGO);
        CreateLevel();
        PositionOfPlayerInDungeon();
    }

    private void PlayerEnterRoom(Room room)
    {
        currentRoom = room;
        if (!currentRoom.roomCompleted)
        {
            //currentRoom.CloseRoom();
        }
    }
}
