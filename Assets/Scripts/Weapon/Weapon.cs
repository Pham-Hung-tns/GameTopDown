using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform shootTrans;
    [SerializeField] protected WeaponData weaponData;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public WeaponData WeaponData => weaponData;
    public virtual void UseWeapon()
    {
        if(animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }
}
