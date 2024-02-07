using UnityEngine;

public class NormalEnemyStates : EnemyStateMachine
{
    [SerializeField] private EnemyWeapon enemyWeapon;
    public EnemyConfig enemyConfig;
    public float rangeToObstacle;
    // khoi tao cac state
    public IdleState idleState { get; private set; }
    public WanderState wanderState { get; private set; }
    public ChaseState chaseState { get; private set; }
    public AttackState attackState { get; private set; }
    public EnemyWeapon EnemyWeapon { get => enemyWeapon; set => enemyWeapon = value; }

    protected override void Start()
    {
        base.Start();
        idleState = new IdleState(this, enemyConfig, this);
        wanderState = new WanderState(this, enemyConfig, this);
        chaseState = new ChaseState(this, enemyConfig,this);
        attackState = new AttackState(this, enemyConfig, this);
        ChangeState(idleState);
    }
    protected override void Update()
    {
        base.Update();
    }
    public override void ChangeDirection(Vector3 newPosition)
    {
        base.ChangeDirection(newPosition);
        Vector3 dir = newPosition - transform.position;
        enemyWeapon.RotateWeaponToPlayer(dir);
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        // Move range
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, enemyConfig.moveRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, rangeToObstacle);

    }
}

