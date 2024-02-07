using UnityEngine;

public class ChaseState : State
{
    NormalEnemyStates normalEnemy;
    EnemyConfig enemyConfig;
    float timer;

    public ChaseState(EnemyStateMachine enemy, EnemyConfig enemyConfig, NormalEnemyStates normalEnemy) : base(enemy)
    {
        this.enemyConfig = enemyConfig;
        this.normalEnemy = normalEnemy;
    }

    public override void OnEnter()
    {
        timer = enemyConfig.timeToAttack;
        normalEnemy.Rb.velocity = Vector2.zero;
    }

    public  override void OnExit()
    {

    }

    public  override void OnUpdate()
    {
        if (!normalEnemy.DetectPlayerInRange())
        {
            normalEnemy.ChangeState(normalEnemy.idleState);
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                normalEnemy.ChangeState(normalEnemy.attackState);
            }
            else
            {
                normalEnemy.ChangeDirection(normalEnemy.Player.position);

            }
        }
    }
}

