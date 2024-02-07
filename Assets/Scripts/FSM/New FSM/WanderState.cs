using UnityEngine;

public class WanderState : State
{
    NormalEnemyStates normalEnemy;
    EnemyConfig enemyConfig;
    float timer;
    private Vector3 movePosition;
    private Vector3 moveDirection;
    public WanderState(EnemyStateMachine enemy, EnemyConfig enemyConfig, NormalEnemyStates normalEnemy) : base(enemy)
    {
        this.normalEnemy = normalEnemy;
        this.enemyConfig = enemyConfig;
    }

    public override void OnEnter()
    {
        timer = enemyConfig.timeToIdle;
        normalEnemy.ChangeAnim(EnemyStateMachine.WANDER_STATE);
        movePosition = normalEnemy.transform.position + GetRandomDirection();
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {
        if (normalEnemy.DetectPlayerInRange())
        {
            normalEnemy.ChangeState(normalEnemy.chaseState);
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                normalEnemy.ChangeState(normalEnemy.idleState);

            }
            else
            {
                moveDirection = (movePosition - normalEnemy.transform.position).normalized;
                normalEnemy.transform.Translate(moveDirection * (enemyConfig.speed * Time.deltaTime));
                normalEnemy.ChangeDirection(movePosition);
                if (CanGetNewPosition(normalEnemy) || Vector3.Distance(normalEnemy.transform.position, movePosition) <= normalEnemy.rangeToObstacle)
                {
                    Vector3 oppositeDir = -moveDirection;
                    normalEnemy.transform.position += oppositeDir * 0.2f;
                    movePosition = normalEnemy.transform.position + GetRandomDirection();
                }
                
            }
        }
    }

    private bool CanGetNewPosition(NormalEnemyStates enemy)
    {
        //if (Vector3.Distance(enemy.transform.position, destination) <= normalEnemy.rangeToObstacle)
        //{
        //    return true;
        //}
        Collider2D collider = Physics2D.OverlapCircle(enemy.transform.position, normalEnemy.rangeToObstacle, enemyConfig.obstacleLayer);
        if (collider != null)
        {
            return true;
        }
        return false;

    }
    private Vector3 GetRandomDirection()
    {
        float xPosition = Random.Range(-enemyConfig.moveRange.x, enemyConfig.moveRange.x);
        float yPosition = Random.Range(-enemyConfig.moveRange.y, enemyConfig.moveRange.y);
        return new Vector3 (xPosition, yPosition);
    }
}

