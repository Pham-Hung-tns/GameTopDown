using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon")]



public class WeaponData : ItemData
{
    public enum WeaponType
    {
        Melee,
        Gun,
        Staff
    }

    public enum WeaponRarity
    {
        Normal,
        Rare,
        Epic, // su thi
        Legend
    }

    [Header("Stat")]
    public int damage;
    public int energy;

    public WeaponType weaponType;
    public WeaponRarity weaponRarity;

    // do lech cua dan
    public float minSpread;
    public float maxSpread;

    public float timeBetweenAttacks;
    public Weapon weapon;
    public override void PickUp()
    {
        LevelManager.Instance.SelectedPlayer.GetComponent<PlayerWeapon>().EquipWeapon(weapon);
    }
}
