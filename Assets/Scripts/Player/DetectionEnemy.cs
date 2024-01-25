using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DetectionEnemy : MonoBehaviour
{
    [SerializeField] private float rangeDetect;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask obstaceLayer;
    private RaycastHit2D[] hit = new RaycastHit2D[100];
    private List<GameObject> EnemyInSight = new List<GameObject>();
    public EnemyHealth EnemyTarget { get; private set; } = null;

    private void Update()
    {
        DetectEnemyInRange();
        DetectObstace();
        CheckDistanceToEnemies();
    }

    private void DetectEnemyInRange()
    {
        hit =  Physics2D.CircleCastAll(transform.position, rangeDetect,Vector2.one, rangeDetect, enemyLayer);
        if(hit.Length == 0)
            EnemyTarget = null;
    }

    private void DetectObstace()
    {
        if(hit.Length >0)
        {
            foreach(var enemy in hit)
            {
                Vector3 dirToEnemy = enemy.collider.transform.position - transform.position;
                RaycastHit2D obstaceHit = Physics2D.Raycast(transform.position, dirToEnemy, dirToEnemy.magnitude, obstaceLayer);
                if(obstaceHit.collider == null)
                {
                    if(EnemyInSight.Contains(enemy.transform.gameObject) == false)
                        EnemyInSight.Add(enemy.transform.gameObject);
                }
                else
                {
                    EnemyTarget = null;
                    if (EnemyInSight.Contains(enemy.transform.gameObject))
                    {
                        EnemyInSight.Remove(enemy.transform.gameObject);
                    }
                }
            }
        }
    }
    private void CheckDistanceToEnemies()
    {
        float minDistance = Mathf.Infinity;

        EnemyHealth enemyTarget = null;
        foreach(var enemy in EnemyInSight)
        {
            float currentDistance = Vector3.Distance( transform.position, enemy.transform.position);
            if(minDistance > currentDistance)
            {
                minDistance = currentDistance;
                enemyTarget = enemy.GetComponent<EnemyHealth>();
            }
        }
        if (enemyTarget != null)
        {
            EnemyTarget = enemyTarget;
            EnemyInSight.Clear();
        }
    }

    private void OnDrawGizmosSelected()
    
    {
        Gizmos.DrawWireSphere(transform.position, rangeDetect);
        if (hit.Length <= 0) return;
        foreach(var i in hit)
        {
            if (i.collider != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, i.collider.transform.position);
            }
        }

        if(EnemyTarget != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, EnemyTarget.transform.position);
        }
    }
}
