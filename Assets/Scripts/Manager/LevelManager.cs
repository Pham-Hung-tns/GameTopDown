using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{


    [Header("Player")]
    [SerializeField] private PlayerMove player;

    [Header("Templates")]
    [SerializeField] private RoomTemplate roomTemplates;
    [SerializeField] private DungeonLibrary dungeonLibrary;
    public RoomTemplate RoomTemplates => roomTemplates;
    public DungeonLibrary DoorSO => dungeonLibrary;
    public PlayerMove Player => player;

    private Room currentRoom;
    private int currentLevelIndex = 0;
    private int currentDungeonIndex = 0;
    private GameObject currentDungeonGO;

    private List<PickableItem> itemsInTheLevel = new List<PickableItem>();


    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        CreateLevel();
    }
    private void CreateLevel()
    {
        currentDungeonGO =  Instantiate(dungeonLibrary.levels[currentLevelIndex].dungeons[currentDungeonIndex], transform);
        itemsInTheLevel = new List<PickableItem>
            (dungeonLibrary.levels[currentLevelIndex].itemsInThisLevel.AvalibleItems);
        PositionOfPlayerInDungeon();
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
        StartCoroutine(IEContinueDungeon());
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
            currentRoom.CloseRoom();
        }
    }

    private IEnumerator IEContinueDungeon()
    {
        UIManager.Instance.FadeNewDungeon(1);
        yield return new WaitForSeconds(2f);
        ContinueDungeonNext();
        UIManager.Instance.FadeNewDungeon(0f);
    }

    public GameObject RandomItemInEachChest()
    {
        int randomIndex = UnityEngine.Random.Range(0, itemsInTheLevel.Count);
        return itemsInTheLevel[randomIndex].gameObject;
    }
}
