using UnityEngine;
public class AttackState : State
{
    NormalEnemyStates normalEnemy;
    EnemyConfig enemyConfig;
    float timer;

    public AttackState(EnemyStateMachine enemy, EnemyConfig enemyConfig, NormalEnemyStates normalEnemy) : base(enemy)
    {
        this.normalEnemy = normalEnemy;
        this.enemyConfig = enemyConfig;
    }

    public override void OnEnter()
    {
        normalEnemy.Rb.velocity = Vector2.zero;
        timer = enemyConfig.timeBetweenAttack;
        normalEnemy.EnemyWeapon.UseWeapon();
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        if (!normalEnemy.DetectPlayerInRange())
        {
            normalEnemy.ChangeState(normalEnemy.idleState);
        }
        else
        {
            timer -= Time.deltaTime;
            normalEnemy.ChangeDirection(normalEnemy.Player.position);
            if (timer <= 0)
            {
                normalEnemy.EnemyWeapon.UseWeapon();
                normalEnemy.ChangeState(normalEnemy.chaseState);
            }
        }
    }
}

