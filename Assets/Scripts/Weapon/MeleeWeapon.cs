using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public override void UseWeapon()
    {
        base.UseWeapon();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.GetComponent<>)
    }

    public override void DestroyWeapon()
    {
        Destroy(gameObject);
    }
}
