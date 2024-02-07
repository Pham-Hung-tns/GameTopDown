
using UnityEngine;

public class BossAttackRandom : State
{
    BossStates boss;
    BossConfig bossConfig;
    float timer;
    float numberOfAttacks;
    public BossAttackRandom(EnemyStateMachine enemy, BossConfig bossConfig, BossStates boss) : base(enemy)
    {
        this.boss = boss;
        this.bossConfig = bossConfig;
    }


    //[SerializeField] private float amountOfProjectile;
    //[SerializeField] private float timeBtwAttack;

    //[SerializeField] private float minSpeed;
    //[SerializeField] private float maxSpeed;

    //[SerializeField] private float minSpawnTime;
    //[SerializeField] private float maxSpawnTime;

    //private EnemyPatterns enemyPatterns;
    //private float timer;
    //private void Awake()
    //{
    //    enemyPatterns = GetComponent<EnemyPatterns>();
    //}

    //private void Start()
    //{
    //    timer = timeBtwAttack;
    //}

    //public override void Act()
    //{
    //    timer -= Time.deltaTime;
    //    if(timer <= 0)
    //    {
    //        float randomBullets = Random.Range(5, amountOfProjectile);
    //        StartCoroutine(IERandomAttack(randomBullets));
    //        timer = timeBtwAttack;
    //    }
    //}

    //private IEnumerator IERandomAttack(float amountBullets)
    //{
    //    for (int i = 0;i < amountBullets ; i++)
    //    {
    //        float randomSpeed = Random.Range(minSpeed, maxSpeed);
    //        Vector3 randomDir = Random.insideUnitCircle.normalized;
    //        Projectile projectile = enemyPatterns.GetProjectTile();
    //        projectile.Speed = randomSpeed;

    //        randomDir = transform.TransformDirection(randomDir);
    //        projectile.Direction = randomDir;
    //        projectile.transform.rotation = Quaternion.Euler(0,0,randomDir.z + 90);

    //        yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
    //    }
    //}
    public override void OnEnter()
    {
        base.OnEnter();
        timer = bossConfig.timeBtwAttack;
        boss.ChangeAnim(EnemyStateMachine.ATTACK_STATE);
        numberOfAttacks = Random.Range(1, 4);
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (numberOfAttacks <= 0)
        {
            boss.ChangeState(boss.idleState);
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                float randomBullets = Random.Range(5, bossConfig.amountOfProjectiles);
                boss.StartCoroutine(boss.IEAttackRandom(randomBullets));
                timer = bossConfig.timeBtwAttack;
                numberOfAttacks -= 1;
            }
        }
    }
}

