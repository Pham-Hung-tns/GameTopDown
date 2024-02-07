using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] protected Transform shootTrans;
    public override void UseWeapon()
    {
        DungeonCM.Instance.ShakeCM(5f, 1f);
        //Create projectile
        Projectile bullet = ObjPoolManager.Instance.Initialization(projectilePrefab);
        float spread =  Random.Range(weaponData.minSpread, weaponData.maxSpread);
        bullet.transform.position = shootTrans.position;
        bullet.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + spread);
        bullet.Direction = transform.right;
        bullet.gameObject.SetActive(true);

        if(Character is PlayerWeapon player)
        {
            bullet.Damage = player.GetDamageCritical();
        }
        else
        {
            bullet.Damage = weaponData.damage;
        }

    }

    public override void DestroyWeapon()
    {
        Destroy(gameObject);
    }
}
