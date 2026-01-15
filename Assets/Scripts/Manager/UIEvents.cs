using System;

public static class UIEvents
{
    // Player stats: curHP, maxHP, curArmor, maxArmor, curEnergy, maxEnergy
    public static Action<float, float, float, float, float, float> OnPlayerStatsChanged;

    // total coins
    public static Action<float> OnCoinChanged;

    // Show weapon UI
    public static Action<Weapon> OnShowWeapon;

    // Start skill cooldown (duration in seconds)
    public static Action<float> OnStartSkillCooldown;

    // Pickup button toggle
    public static Action<bool> OnPickupToggle;

    // Level text update
    public static Action<string> OnLevelTextUpdate;

    // Room completed
    public static Action OnRoomCompleted;

    // Boss health updated (0..1)
    public static Action<float> OnBossHealthUpdated;

    // Fade dungeon (value)
    public static Action<float> OnFadeNewDungeon;

    // Show game over
    public static Action OnShowGameOver;
}
