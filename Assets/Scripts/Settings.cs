using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public static class Settings
{
    #region UNITS
    public const float pixelsPerUnit = 16f;
    public const float tileSizePixels = 16f;
    #endregion

    #region DUNGEON BUILD SETTINGS
    public const int maxDungeonRebuildAttemptsForRoomGraph = 1000;
    public const int maxDungeonBuildAttempts = 10;
    #endregion

    #region ROOM SETTINGS
    public const float fadeInTime = 0.5f; // time to fade in the room
    public const int maxChildCorridors = 3; // Max number of child corridors leading from a room.
    public const float doorUnlockDelay = 1f;
    #endregion

    #region ANIMATOR PARAMETERS
    // Animator parameters - Player

  
    public static readonly int isMoving = Animator.StringToHash("moveMotion");
    public const float baseSpeedForPlayerAnimations = 8f;

    // Animator parameters - Enemy
    public const float baseSpeedForEnemyAnimations = 3f;


    // Animator parameters - Door
    public static readonly int open = Animator.StringToHash("open");

    // Animator parameters - DamageableDecoration
    public static readonly int destroy = Animator.StringToHash("destroy");
    public static readonly string stateDestroyed = "Destroyed";

    #endregion

    #region GAMEOBJECT TAGS
    public const string playerTag = "Player";
    public const string playerWeapon = "playerWeapon";
    #endregion


    #region AUDIO
    public const float musicFadeOutTime = 0.5f;  // Defualt Music Fade Out Transition
    public const float musicFadeInTime = 0.5f;  // Default Music Fade In Transition
    #endregion

    #region FIRING CONTROL
    public const float useAimAngleDistance = 3.5f; // if the target distance is less than this then the aim angle will be used (calculated from player), else the weapon aim angle will be used (calculated from the weapon). 
    #endregion

    #region ASTAR PATHFINDING PARAMETERS
    public const int defaultAStarMovementPenalty = 40;
    public const int preferredPathAStarMovementPenalty = 1;
    public const int targetFrameRateToSpreadPathfindingOver = 60;
    public const float playerMoveDistanceToRebuildPath = 3f;
    public const float enemyPathRebuildCooldown = 2f;

    #endregion

    #region ENEMY PARAMETERS
    public const int defaultEnemyVitality = 20;

    public const string IDLE_STATE = "idle";
    public const string WANDER_STATE = "wander";
    public const string ATTACK_STATE = "attack";
    #endregion

    #region UI PARAMETERS
    public const float uiHeartSpacing = 16f;
    public const float uiAmmoIconSpacing = 4f;
    #endregion

    #region CONTACT DAMAGE PARAMETERS
    public const float contactDamageCollisionResetDelay = 0.5f;
    #endregion

    #region HIGHSCORES
    public const int numberOfHighScoresToSave = 100;
    #endregion

}
