
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayerAction : FSMAction
{
    [SerializeField] private float timeBetweenAttack;
    private EnemyStateMachine enemy;
    private EnemyWeapon weapon;
    private float attackTimer;
    
    private void Awake()
    {
        enemy = GetComponent<EnemyStateMachine>();
        weapon = GetComponent<EnemyWeapon>();
    }

    private void Start()
    {
        attackTimer = timeBetweenAttack;    
    }
  
    public override void Act()
    {
        if(enemy.Player != null)
        {
            attackTimer -= Time.deltaTime;
            if(attackTimer <= 0)
            {
                weapon.UseWeapon();
                attackTimer = timeBetweenAttack;
            }
        }
    }

}
