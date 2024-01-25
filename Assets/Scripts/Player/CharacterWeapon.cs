using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : MonoBehaviour
{
    [SerializeField] protected Transform weaponPos;
    protected Weapon currentWeapon;

    protected int weaponIndex; // 0 - 1
    protected Weapon[] equippedWeapons = new Weapon[2];

    protected SpriteRenderer sp;
    protected virtual void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        if(sp == null )
        {
            sp = GetComponentInChildren<SpriteRenderer>();
        }
    }

    protected void RotateWeapon(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (dir.x > 0f) // Facing Right
        {
            weaponPos.localScale = Vector3.one;
            currentWeapon.transform.localScale = Vector3.one;
            sp.flipX = false;
        }
        else // Facing Left
        {
            weaponPos.localScale = new Vector3(-1, 1, 1);
            currentWeapon.transform.localScale = new Vector3(-1, -1, 1);
            sp.flipX = true;
        }

        currentWeapon.transform.eulerAngles = new Vector3(0f, 0f, angle);

    }
}
