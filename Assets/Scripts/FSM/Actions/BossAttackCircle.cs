//using System;
//using UnityEngine;

//public class BossAttackCircle : State
//{
//    //[SerializeField] private float amountOfProjectile;
//    //[SerializeField] private float timeBtwAttack;

//    //private EnemyPatterns enemyPatterns;
//    //private float timer;
//    //private void Awake()
//    //{
//    //    enemyPatterns = GetComponent<EnemyPatterns>();
//    //}

//    //private void Start()
//    //{
//    //    timer = timeBtwAttack;
//    //}
//    //public override void Act()
//    //{
//    //    Attack();
//    //}

//    //private void Attack()
//    //{
//    //    timer -= Time.deltaTime;
//    //    if (timer <= 0)
//    //    {
//    //        float angle = 360 / amountOfProjectile;
//    //        for (int i = 0; i < amountOfProjectile; i++)
//    //        {
//    //            float projectAngle = angle * i;
//    //            Projectile projectile = enemyPatterns.GetProjectTile();
//    //            projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, projectAngle));
//    //        }
//    //    }
//    //    timer = timeBtwAttack;
//    //}
//    BossStates boss;
//    public BossAttackCircle(EnemyStateMachine enemy, EnemyConfig enemyConfig, BossStates boss) : base(enemy, enemyConfig)
//    {
//        this.boss = boss;
//    }

//    public override void OnEnter()
//    {
//        base.OnEnter();
//    }

//    public override void OnExit()
//    {
//        base.OnExit();
//    }

//    public override void OnUpdate()
//    {
//        base.OnUpdate();
//    }
//}
