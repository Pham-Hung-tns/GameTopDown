using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAction : FSMAction
{
    [Header("Config")]
    [SerializeField] private bool useRandomWander;
    [SerializeField] private bool useDebug;

    [Header("Movement")]
    [SerializeField] private float wanderSpeed;
    [SerializeField] private float minDistanceCheck;
    [SerializeField] private Vector2 moveRange;

    [Header("Obstace")]
    [SerializeField] private LayerMask obstaceLayer;
    [SerializeField] private float rangeCanDetectObstacle;

    private Vector3 movePos;
    private Vector3 moveDir;

    private void Start()
    {
        GetNewRandomPosition();
    }
    public override void Act()
    {
        moveDir = (movePos - transform.position).normalized;
        transform.Translate(moveDir * (wanderSpeed * Time.deltaTime));
        if(CanGetNewPosition())
        {
            GetNewRandomPosition();
        }
    }

    private void GetNewRandomPosition()
    {
        if (useRandomWander)
            movePos = transform.position + GetRandomDirection();
    }

    private Vector3 GetRandomDirection()
    {
        float randomX = Random.Range(-moveRange.x, moveRange.x);
        float randomY = Random.Range(-moveRange.y, moveRange.y);
        return new Vector3(randomX, randomY);
    }

    private bool CanGetNewPosition()
    {
        if(Vector3.Distance(transform.position, movePos) <= minDistanceCheck)
        {
            return true;
        }

        Collider2D collider = Physics2D.OverlapCircle(transform.position, rangeCanDetectObstacle, obstaceLayer);
        if(collider != null)
        {
            //(1,1);
            Vector3 oppositeDir = -moveDir;
            transform.position += oppositeDir * 0.1f;
            return true;
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        if (!useDebug) return;
        if(useRandomWander)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, moveRange * 2f);
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, movePos);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, rangeCanDetectObstacle);
    }
}
