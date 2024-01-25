using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected const string LAYER_PLAYER = "Player";
    protected const string LAYER_ENEMY = "Enemy";

    [SerializeField] protected WeaponData weaponData;

    private Animator animator;
    public CharacterWeapon Character { get; set; }
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
    public virtual void DestroyWeapon()
    {

    }
}
