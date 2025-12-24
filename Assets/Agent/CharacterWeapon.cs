using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : MonoBehaviour
{
    [SerializeField] protected Transform weaponPosition;
    
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

    // vì việc khởi tạo vũ khí sẽ nằm trong file controller nên hàm này sẽ public để các controller có thể gọi
    public virtual void CreateWeapon(Weapon weaponPrefab)
    {
        
    }

    protected void RotateWeaponToAgent(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        FlipSprite(angle > 90 || angle < -90);
        currentWeapon.transform.eulerAngles = new Vector3(0f, 0f, angle);
    }

    private void FlipSprite(bool val)
    {
        int flipModifier = val ? -1 : 1;
        sp.flipX = val;
        weaponPosition.localScale = new Vector3(weaponPosition.localScale.x, flipModifier * Mathf.Abs(weaponPosition.localScale.y), weaponPosition.localScale.z);
    }

}
