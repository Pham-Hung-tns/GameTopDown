using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public static event Action OnRoomCompleted;
    public static event Action<float> OnPlayerInRoomBoss;

    public GameObject SelectedPlayer { get; set; }

    [Header("Templates")]
    //[SerializeField] private RoomTemplate roomTemplates;
    //[SerializeField] private DungeonLibrary dungeonLibrary;
    // public RoomTemplate RoomTemplates => roomTemplates;
    //public DungeonLibrary DoorSO => dungeonLibrary;

    // private Room currentRoom;
    private int currentLevelIndex = 0;
    private int currentDungeonIndex = 0;
    private int amountOfEnemies;
    private GameObject currentDungeonGO;

    private List<PickableItem> itemsInTheLevel = new List<PickableItem>();


    protected override void Awake()
    {
        base.Awake();
        CreatePlayerInDungeon();

    }
    private void Start()
    {
        CreateLevel();
    }
    private void CreateLevel()
    {
        //currentDungeonGO =  Instantiate(dungeonLibrary.levels[currentLevelIndex].dungeons[currentDungeonIndex], transform);
        //itemsInTheLevel = new List<PickableItem>
        //    (dungeonLibrary.levels[currentLevelIndex].itemsInThisLevel.AvalibleItems);
        //PositionOfPlayerInDungeon();
    }
    public string GetCurrentLevelText()
    {
        //return $"Level {dungeonLibrary.levels[currentLevelIndex].levelName} - {dungeonLibrary.levels.Length}";
        return "Fix sau";
    }
    private void CreatePlayerInDungeon()
    {
        if(GameManager.Instance.playerPrefab != null)
        {
            SelectedPlayer =  Instantiate(GameManager.Instance.playerPrefab.playerPrefab);
            PlayerConfig player = SelectedPlayer.GetComponent<PlayerController>().PlayerData;
            SetStatWhenStart(player);
        }
    }
    //sau dung Json doc data
    public void SetStatWhenStart(PlayerConfig playerConfig)
    {
        playerConfig.currentHealth = playerConfig.MaxHealth;
        playerConfig.currentArmor = playerConfig.MaxArmor;
        playerConfig.currentEnergy = playerConfig.MaxEnergy;
    }
    //private void CreateBoss(Room room)
    //{
    //    //Vector3 tilePos = ;
    //    EnemyStateMachine boss = Instantiate(dungeonLibrary.levels[currentLevelIndex].Boss, room.PositionOfBoss(), Quaternion.identity, currentRoom.transform);
    //    boss.CurrentRoom = room;
    //}


    //private void CreateEnemies()
    //{
    //    int amount = GetAmountOfEnemies();
    //    amountOfEnemies = amount;
    //    for(int i = 0; i < amountOfEnemies; i++)
    //    {
    //        Vector3 tilePos = currentRoom.GetTilePosition();
    //        NormalEnemyStates enemy = Instantiate(GetEnemies(), tilePos, Quaternion.identity, currentRoom.transform);
    //        enemy.CurrentRoom = currentRoom;
    //    } 
    //}

    //private NormalEnemyStates GetEnemies()
    //{
    //    NormalEnemyStates[] enemies = dungeonLibrary.levels[currentLevelIndex].enemies;
    //    int randomIndex = UnityEngine.Random.Range(0, enemies.Length);
    //    return enemies[randomIndex];
    //}

    //private int GetAmountOfEnemies()
    //{
    //    int amount = UnityEngine.Random.Range(dungeonLibrary.levels[currentLevelIndex].minEnemiesPerRoom, 
    //        dungeonLibrary.levels[currentLevelIndex].maxEnemiesPerRoom);
    //    return amount;
    //}

    //private void CreateChestWhenCompleted()
    //{
    //    Vector3 chestPos = currentRoom.GetTilePosition();
    //    Instantiate(dungeonLibrary.chest, chestPos, Quaternion.identity, currentRoom.transform);
    //}

    private void PortalEventCallBack()
    {
        StartCoroutine(IEContinueDungeon());
    }

    public void PositionOfPlayerInDungeon()
    {
        //Room[] dungeonRooms = currentDungeonGO.GetComponentsInChildren<Room>();
        //Room posDefault = null;
        //foreach (Room room in dungeonRooms)
        //{
        //    if(room.RoomType == RoomType.RoomEntrance)
        //    {
        //        posDefault = room;
        //    }
        //}

        //if(posDefault != null)
        //{
        //    if(SelectedPlayer != null)
        //    {
        //        SelectedPlayer.transform.position = posDefault.transform.position;
        //    }
        //}
        AudioManager.Instance.PlayMusic("Theme");
    }

    private void ContinueNextLevel()
    {
        //currentDungeonIndex++;
        //if (currentDungeonIndex >= dungeonLibrary.levels[currentLevelIndex].dungeons.Length)
        //{
        //    currentLevelIndex++;
        //    currentDungeonIndex = 0;
        //}
        //Destroy(currentDungeonGO);
        //CreateLevel();
        //PositionOfPlayerInDungeon();
    }

    private void PlayerEnterRoom() //parameter: Room room)
    {
        AudioManager.Instance.PlaySFX("Door_Close");
        AudioManager.Instance.PlayMusic("Battle");
        //currentRoom = room;
        //if (!currentRoom.roomCompleted)
        //{
        //    currentRoom.CloseRoom();
        //    switch (currentRoom.RoomType)
        //    {
        //        case RoomType.RoomEnemy:
        //            {
        //                CreateEnemies();
        //                break;
        //            }
        //        case RoomType.RoomBoss:
        //            {
        //                EnemyHealth boss = currentRoom.GetComponentInChildren<EnemyHealth>();
        //                OnPlayerInRoomBoss?.Invoke(boss.Health);
        //                break;
        //            }
        //    }
        //}
    }

    private IEnumerator IEContinueDungeon()
    {
        UIManager.Instance.FadeNewDungeon(1);
        yield return new WaitForSeconds(2f);
        ContinueNextLevel();
        UIManager.Instance.UpdateLevelText(GetCurrentLevelText());
        UIManager.Instance.FadeNewDungeon(0f);
    }

    public GameObject RandomItemInEachChest()
    {
        int randomIndex = UnityEngine.Random.Range(0, itemsInTheLevel.Count);
        return itemsInTheLevel[randomIndex].gameObject;
    }

    private void EnemyKilledBack(Transform enemyPos)
    {
        //amountOfEnemies -= 1;
        //CreateBonus(enemyPos);
        //if(amountOfEnemies <= 0)
        //{
        //    if(currentRoom.roomCompleted == false)
        //    {
        //        AudioManager.Instance.PlaySFX("Door_Open");
        //        amountOfEnemies = 0;
        //        currentRoom.SetRoomCompleted();
        //        CreateChestWhenCompleted();
        //        OnRoomCompleted?.Invoke();
        //    }
        //    if(currentRoom.RoomType == RoomType.RoomBoss)
        //    {
        //        CreatePortal();
        //    }
        //}
    }

    private void CreatePortal()
    {
        //Vector3 pos = currentRoom.GetTilePosition();
        //Instantiate(dungeonLibrary.portal, pos, Quaternion.identity, currentRoom.transform);
    }

    private void CreateBonus(Transform enemyPos)
    {
        //int amount = UnityEngine.Random.Range(dungeonLibrary.levels[currentLevelIndex].minBonus,
        //    dungeonLibrary.levels[currentLevelIndex].maxBonus);
        //for (int i = 0; i < amount; i++)
        //{
        //    int bonusRandom = UnityEngine.Random.Range(0, dungeonLibrary.bonus.Length);
        //    Vector3 bonusRange = UnityEngine.Random.insideUnitCircle.normalized * dungeonLibrary.range;
        //    Instantiate(dungeonLibrary.bonus[bonusRandom], enemyPos.position + bonusRange, Quaternion.identity);
        //}
    }

    private void OnEnable()
    {
        // Room.OnPlayerEnterTheRoom += PlayerEnterRoom;
        EnemyHealth.OnEnemyKilledEvent += EnemyKilledBack;
        Portal.OnNextDungeon += PortalEventCallBack;
    }

    private void OnDisable()
    {
        // Room.OnPlayerEnterTheRoom -= PlayerEnterRoom;
        EnemyHealth.OnEnemyKilledEvent += EnemyKilledBack;
        Portal.OnNextDungeon -= PortalEventCallBack;
    }
}
