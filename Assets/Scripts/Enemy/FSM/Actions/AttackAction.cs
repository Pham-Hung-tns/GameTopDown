using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    private Transform player;
    private float attackDuration = 0f;
    public override void OnEnter()
    {
        enemyBrain.TimeLimit = enemyBrain.EnemyConfig.timeToIdle;
        enemyBrain.CanAttack = true;
        // change to attack animation
    }

    public override void OnUpdate()
    {
        attackDuration -= Time.deltaTime;
        if(attackDuration <= 0f && enemyBrain.CanAttack != false)
        {
            enemyBrain.EnemyWeapon.StartAttack();
            attackDuration = enemyBrain.EnemyConfig.timeBetweenAttack;
        }
    }

    public override void OnExit()
    {
        enemyBrain.Player = null;
        enemyBrain.CanAttack = false;
    }
}
