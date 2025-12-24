using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected const string LAYER_PLAYER = "Player";
    protected const string LAYER_ENEMY = "Enemy";

    [SerializeField] protected WeaponData weaponData;

    private Animator animatorator;
    public CharacterWeapon Character { get; set; }
    private void Awake()
    {
        animatorator = GetComponent<Animator>();
    }
    public WeaponData WeaponData => weaponData;
    public virtual void UseWeapon()
    {
        if(animatorator != null)
        {
            animatorator.SetTrigger("Attack");
        }
    }
    public virtual void DestroyWeapon()
    {

    }
}
