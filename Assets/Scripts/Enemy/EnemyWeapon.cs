using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : CharacterWeapon
{
    [SerializeField] private Weapon initalWeapon;

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        CreateWeapon();
    }
    private void CreateWeapon()
    {
        currentWeapon = Instantiate(initalWeapon, weaponPosition.position, Quaternion.identity, 
            weaponPosition);
    }
    public void UseWeapon()
    {
        currentWeapon.UseWeapon();
    }
    private void Update()
    {
        //if (LevelManager.Instance.SelectedPlayer == null) { return; }
        //Vector3 dir = LevelManager.Instance.SelectedPlayer.transform.position - transform.position;
        //RotateWeapon(dir);
    }

    public void RotateWeaponToPlayer(Vector3 dir)
    {
        RotateWeaponToAgent(dir);
    }

    public void DestroyWeapon()
    {

    }
}
