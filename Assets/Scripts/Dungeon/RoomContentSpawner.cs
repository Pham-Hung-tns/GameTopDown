using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helper class ?? spawn enemies và chests trong room
/// Dùng cho c? editor mode (TestDungeonBuilderEditor) và runtime mode (LevelManager)
/// </summary>
public class RoomContentSpawner
{
    /// <summary>
    /// Spawn enemies trong m?t room
    /// </summary>
    public static int SpawnEnemiesInRoom(Room room, DungeonLevelSO dungeonLevel)
    {
        if (room == null || room.instantiatedRoom == null)
        {
            Debug.LogWarning("Room ho?c instantiatedRoom là null, không th? spawn enemy");
            return 0;
        }

        // Skip corridor và entrance
        if (room.roomNodeType.isCorridor || room.roomNodeType.isEntrance)
            return 0;

        // Ki?m tra spawn position
        if (room.spawnPositionArray == null || room.spawnPositionArray.Length == 0)
        {
            Debug.LogWarning($"Room {room.id} không có spawn positions");
            return 0;
        }

        // Ki?m tra enemy data
        if (room.enemiesByLevelList == null || room.enemiesByLevelList.Count == 0)
        {
            Debug.LogWarning($"Room {room.id} không có enemy data");
            return 0;
        }

        // L?y spawn parameters
        RoomEnemySpawnParameters spawnParams = room.GetRoomEnemySpawnParameters(dungeonLevel);
        if (spawnParams == null)
        {
            Debug.LogWarning($"Room {room.id} không có spawn parameters cho level {dungeonLevel.name}");
            return 0;
        }

        // Tính s? l??ng enemy c?n spawn
        int totalToSpawn = Random.Range(spawnParams.minTotalEnemiesToSpawn, spawnParams.maxTotalEnemiesToSpawn + 1);

        int enemiesSpawned = 0;
        for (int i = 0; i < totalToSpawn; i++)
        {
            EnemyDetailsSO enemyDetails = SelectRandomEnemy(room, dungeonLevel);
            if (enemyDetails == null || enemyDetails.enemyPrefab == null)
                continue;

            Vector3 spawnWorldPos = GetRandomSpawnWorldPosition(room);

            GameObject enemy = Object.Instantiate(enemyDetails.enemyPrefab, spawnWorldPos, Quaternion.identity, room.instantiatedRoom.transform);
            if (enemy != null)
            {
                enemy.name = $"{enemyDetails.name}_{enemiesSpawned}";

                // Áp d?ng health theo level n?u có
                EnemyVitality vitality = enemy.GetComponent<EnemyVitality>();
                if (vitality != null)
                {
                    int health = GetHealthForLevel(enemyDetails, dungeonLevel, (int)vitality.Health);
                    vitality.Health = health;
                }

                // Áp d?ng EnemyController n?u có
                EnemyController controller = enemy.GetComponent<EnemyController>();
                if (controller != null && enemyDetails != null)
                {
                    // EnemyDetailsSO ch?a prefab, còn EnemyConfig là c?u hình riêng
                    // Do b?n l?y t? RoomTemplateSO, EnemyConfig có th? ???c set t? n?i khác
                    // T?m th?i b? qua vi?c set EnemyConfig ? ?ây
                }

                enemiesSpawned++;
            }
        }

        if (enemiesSpawned > 0)
        {
            Debug.Log($"Spawned {enemiesSpawned} enemies trong room {room.id}");
        }

        return enemiesSpawned;
    }

    /// <summary>
    /// Spawn chests trong m?t room
    /// </summary>
    public static int SpawnChestsInRoom(Room room, DungeonLevelSO dungeonLevel)
    {
        if (room == null || room.instantiatedRoom == null)
            return 0;

        // Skip corridor và entrance
        if (room.roomNodeType.isCorridor || room.roomNodeType.isEntrance)
            return 0;

        // Ki?m tra spawn position
        if (room.spawnPositionArray == null || room.spawnPositionArray.Length == 0)
            return 0;

        // Tìm chest prefab
        GameObject chestPrefab = Resources.Load<GameObject>("Chest");
        if (chestPrefab == null)
        {
            GameResources gameResources = Resources.Load<GameResources>("GameResources");
            if (gameResources != null && gameResources.chestItemPrefab != null)
            {
                chestPrefab = gameResources.chestItemPrefab;
            }
        }

        if (chestPrefab == null)
        {
            Debug.LogWarning("Không tìm th?y chest prefab");
            return 0;
        }

        // Roll xác su?t spawn chest (25-50%)
        float chance = Random.Range(0.25f, 0.5f);
        if (Random.value > chance)
            return 0;

        // Ch?n v? trí spawn
        Vector3 spawnWorldPos = GetRandomSpawnWorldPosition(room);
        GameObject chest = Object.Instantiate(chestPrefab, spawnWorldPos, Quaternion.identity, room.instantiatedRoom.transform);
        if (chest != null)
        {
            Debug.Log($"Spawned chest trong room {room.id}");
            return 1;
        }

        return 0;
    }

    /// <summary>
    /// Ch?n enemy ng?u nhiên d?a trên weighted ratio
    /// </summary>
    private static EnemyDetailsSO SelectRandomEnemy(Room room, DungeonLevelSO level)
    {
        if (room.enemiesByLevelList == null)
            return null;

        SpawnableObjectsByLevel<EnemyDetailsSO> byLevel = room.enemiesByLevelList.Find(e => e.dungeonLevel == level);
        if (byLevel == null || byLevel.spawnableObjectRatioList == null || byLevel.spawnableObjectRatioList.Count == 0)
            return null;

        int totalWeight = 0;
        foreach (var ratio in byLevel.spawnableObjectRatioList)
            totalWeight += Mathf.Max(0, ratio.ratio);

        if (totalWeight <= 0)
            return null;

        int roll = Random.Range(0, totalWeight);
        int cumulative = 0;
        foreach (var ratio in byLevel.spawnableObjectRatioList)
        {
            cumulative += Mathf.Max(0, ratio.ratio);
            if (roll < cumulative)
                return ratio.dungeonObject;
        }

        return byLevel.spawnableObjectRatioList.Count > 0 ? byLevel.spawnableObjectRatioList[0].dungeonObject : null;
    }

    /// <summary>
    /// L?y v? trí spawn world t? spawn position array
    /// </summary>
    private static Vector3 GetRandomSpawnWorldPosition(Room room)
    {
        if (room.spawnPositionArray == null || room.spawnPositionArray.Length == 0)
            return room.instantiatedRoom.transform.position;

        Vector2Int spawnCell = room.spawnPositionArray[Random.Range(0, room.spawnPositionArray.Length)];

        // spawnCell là local coordinates c?a room template
        // Công th?c: worldPos = spawnCell + room.lowerBounds - room.templateLowerBounds
        Vector3 worldPos = new Vector3(
            spawnCell.x + room.lowerBounds.x - room.templateLowerBounds.x,
            spawnCell.y + room.lowerBounds.y - room.templateLowerBounds.y,
            0f
        );

        // Center c?a tile
        worldPos += new Vector3(0.5f, 0.5f, 0f);

        return worldPos;
    }

    /// <summary>
    /// L?y health d?a theo level
    /// </summary>
    private static int GetHealthForLevel(EnemyDetailsSO details, DungeonLevelSO level, int fallback)
    {
        if (details.healthByLevel != null)
        {
            foreach (var h in details.healthByLevel)
            {
                if (h != null && h.dungeonLevel == level)
                    return h.health;
            }
        }
        return fallback;
    }
}
