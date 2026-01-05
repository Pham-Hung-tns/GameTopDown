using UnityEngine;


public class WeaponDataSO : ItemDataSO
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
    public Transform firePoint; // Điểm bắn
    public float cooldown;
    public Weapon weapon;
    public override void PickUp()
    {
        LevelManager.Instance.SelectedPlayer.GetComponent<PlayerWeapon>().EquipWeapon(weapon);
    }
}
