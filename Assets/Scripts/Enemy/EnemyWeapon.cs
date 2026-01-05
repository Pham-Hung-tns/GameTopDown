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
        equippedWeapons[weaponIndex] = currentWeapon;
        equippedWeapons[weaponIndex].Character = this;
    }
    //public void UseWeapon()
    //{
    //    if (currentWeapon == null) return;
    //    // choose weapon type for animator
    //    int type = (currentWeapon.WeaponData is RangeWeaponDataSO rd && rd.canCharge) ? 1 : 0;
    //    currentWeapon.SetAnimatorWeaponType(type);
    //    currentWeapon.TriggerAttackAnimation();
    //    currentWeapon.ExecuteAttack(1f);
    //}

    public void RotateWeaponToPlayer(Vector3 dir)
    {
        RotateWeaponToAgent(dir);
    }

    public void DestroyWeapon()
    {

    }
}
