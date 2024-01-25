using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPatternAction : FSMAction
{
    [SerializeField] private float amountOfProjectile;
    [SerializeField] private float timeBtwAttack;

    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;

    private EnemyPatterns enemyPatterns;
    private float timer;
    private void Awake()
    {
        enemyPatterns = GetComponent<EnemyPatterns>();
    }

    private void Start()
    {
        timer = timeBtwAttack;
    }

    public override void Act()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            StartCoroutine(IERandomAttack());
            timer = timeBtwAttack;
        }
    }

    private IEnumerator IERandomAttack()
    {
        for (int i = 0;i < amountOfProjectile; i++)
        {
            float randomSpeed = Random.Range(minSpeed, maxSpeed);
            Vector3 randomDir = Random.insideUnitCircle.normalized;

            Projectile projectile = enemyPatterns.GetProjectTile();
            projectile.Speed = randomSpeed;
            projectile.Direction = randomDir;
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        }
    }
}
