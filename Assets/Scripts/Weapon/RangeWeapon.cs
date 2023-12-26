using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] protected Transform shootTrans;
    public override void UseWeapon()
    {
        //Create projectile
        Projectile bullet = Instantiate(projectilePrefab);
        bullet.transform.position = shootTrans.position;
        bullet.Direction = shootTrans.right;

        //tao do lech cua duong dan
        float spread =  Random.Range(weaponData.minSpread, weaponData.maxSpread);
        bullet.transform.Rotate(0,0,transform.rotation.eulerAngles.z);

    }
}
