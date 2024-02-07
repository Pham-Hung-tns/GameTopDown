using UnityEngine;
using System;
public class IdleState : State
{
    NormalEnemyStates normalEnemy;
    EnemyConfig enemyConfig;
    private float timer;

    public IdleState(EnemyStateMachine enemy, EnemyConfig enemyConfig, NormalEnemyStates normalEnemy) : base(enemy)
    {
        this.normalEnemy = normalEnemy;
        this.enemyConfig = enemyConfig;
    }

    public override void OnEnter()
    {
        timer = enemyConfig.timeToWander;
        normalEnemy.ChangeAnim(EnemyStateMachine.IDLE_STATE);
        normalEnemy.Rb.velocity = Vector3.zero;
    }

    public override void OnExit()
    {
        
    }

    public  override void OnUpdate()
    {
        if (normalEnemy.DetectPlayerInRange())
        {
            normalEnemy.ChangeState(normalEnemy.chaseState);
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                normalEnemy.ChangeState(normalEnemy.wanderState);
            }
        }
    }
}

