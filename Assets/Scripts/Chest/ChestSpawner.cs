using UnityEngine;

/// <summary>
/// Handles chest spawn logic when rooms are entered or cleared.
/// </summary>
public class ChestSpawner : MonoBehaviour
{
    [Header("Chest Setup")]
    public GameObject chestPrefab;
    [Range(0f, 1f)] public float chestSpawnChanceMin = 0.25f;
    [Range(0f, 1f)] public float chestSpawnChanceMax = 0.5f;
    public ChestSpawnEvent chestSpawnEvent = ChestSpawnEvent.OnEnemiesDefeated;
    public ChestSpawnPosition chestSpawnPosition = ChestSpawnPosition.AtSpawnerPosition;

    [Header("Loot")]
    public SpawnableObjectsByLevel<WeaponData> weaponSpawnByLevel;
    public SpawnableObjectsByLevel<HealthLootRange> healthSpawnByLevel;
    public SpawnableObjectsByLevel<AmmoLootRange> ammoSpawnByLevel;
    public int numberOfItemsToSpawnMin = 1;
    public int numberOfItemsToSpawnMax = 2;

    private Room owningRoom;

    private void Awake()
    {
        owningRoom = GetComponentInParent<InstantiatedRoom>()?.room;
    }

    private void OnEnable()
    {
        StaticEventHandler.OnRoomChanged += HandleRoomChanged;
        StaticEventHandler.OnRoomEnemiesDefeated += HandleRoomDefeated;
    }

    private void OnDisable()
    {
        StaticEventHandler.OnRoomChanged -= HandleRoomChanged;
        StaticEventHandler.OnRoomEnemiesDefeated -= HandleRoomDefeated;
    }

    private void HandleRoomChanged(RoomChangedEventArgs args)
    {
        if (chestSpawnEvent != ChestSpawnEvent.OnRoomEntry) return;
        if (args.room != owningRoom) return;
        TrySpawnChest();
    }

    private void HandleRoomDefeated(Room room)
    {
        if (chestSpawnEvent != ChestSpawnEvent.OnEnemiesDefeated) return;
        if (room != owningRoom) return;
        TrySpawnChest();
    }

    private void TrySpawnChest()
    {
        if (chestPrefab == null) return;

        float chance = Random.Range(chestSpawnChanceMin, chestSpawnChanceMax);
        if (Random.value > chance) return;

        Vector3 spawnPos = GetSpawnPosition();
        Instantiate(chestPrefab, spawnPos, Quaternion.identity, transform.parent);
    }

    private Vector3 GetSpawnPosition()
    {
        if (chestSpawnPosition == ChestSpawnPosition.AtPlayerPosition && LevelManager.Instance != null && LevelManager.Instance.SelectedPlayer != null)
        {
            return LevelManager.Instance.SelectedPlayer.transform.position;
        }

        return transform.position;
    }
}

public enum ChestSpawnEvent
{
    OnRoomEntry,
    OnEnemiesDefeated
}

public enum ChestSpawnPosition
{
    AtSpawnerPosition,
    AtPlayerPosition
}

[System.Serializable]
public class HealthLootRange
{
    public int minPercent = 10;
    public int maxPercent = 25;
}

[System.Serializable]
public class AmmoLootRange
{
    public int minPercent = 10;
    public int maxPercent = 25;
}

