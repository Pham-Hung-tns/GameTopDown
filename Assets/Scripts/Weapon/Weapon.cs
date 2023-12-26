using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponData weaponData;

    private Animator animator;
    public WeaponData WeaponData => weaponData;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public virtual void UseWeapon()
    {
        if(animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void DestroyWeapon()
    {
        Destroy(gameObject);
    }
}
