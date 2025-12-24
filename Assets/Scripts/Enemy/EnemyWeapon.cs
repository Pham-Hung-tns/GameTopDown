using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : CharacterWeapon
{
    protected override void Awake()
    {
        base.Awake();
    }
  
    public override void CreateWeapon(Weapon initalWeapon)
    {
        currentWeapon = Instantiate(initalWeapon, weaponPosition.position, Quaternion.identity, 
            weaponPosition);
    }
    public void UseWeapon()
    {
        currentWeapon.UseWeapon();
    }

    public void RotateWeaponToPlayer(Vector3 dir)
    {
        RotateWeaponToAgent(dir);
    }

    public void DestroyWeapon()
    {

    }
}
