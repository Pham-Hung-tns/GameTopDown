using UnityEngine;
using System;
using System.Collections;

public class BossStates : EnemyStateMachine
{
    [SerializeField] private EnemyPatterns enemyPatterns;
    [SerializeField] private BossConfig bossConfig;
    public Boss_IdleState idleState { get; private set; }
    public BossAttackRandom attackRandomState { get; private set; }
    public EnemyPatterns EnemyPatterns { get; private set; }

    protected override void Start()
    {
        base.Start();
        idleState = new Boss_IdleState(this, bossConfig, this);
        attackRandomState = new BossAttackRandom(this, bossConfig, this);
        ChangeState(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
    public IEnumerator IEAttackRandom(float amountBullets)
    {
        for (int i = 0; i < amountBullets; i++)
        {
            float randomSpeed = UnityEngine.Random.Range(bossConfig.minSpeed, bossConfig.maxSpeed);
            Vector3 randomDir = UnityEngine.Random.insideUnitCircle.normalized;
            Projectile projectile = enemyPatterns.GetProjectTile();
            projectile.Direction = randomDir;
            projectile.Speed = randomSpeed;

            //randomDir = transform.TransformDirection(randomDir);
            //projectile.Direction = randomDir;
            //projectile.transform.rotation = Quaternion.Euler(0, 0, randomDir.z + 90);

            yield return new WaitForSeconds(UnityEngine.Random.Range(bossConfig.minSpawnTime, bossConfig.maxSpawnTime));
        }
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}

