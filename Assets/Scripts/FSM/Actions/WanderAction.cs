using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAction : FSMAction
{
    [Header("Config")]
    [SerializeField] private bool useRandomWander;
    [SerializeField] private bool useDebug;
    [SerializeField] private bool useTilesPosition;

    [Header("Movement")]
    [SerializeField] private float wanderSpeed;
    [SerializeField] private float minDistanceCheck;
    [SerializeField] private Vector2 moveRange;

    [Header("Obstace")]
    [SerializeField] private LayerMask obstaceLayer;
    [SerializeField] private float rangeCanDetectObstacle;

    [Header("Idle")]
    [SerializeField] private float timeToIdle;
    private Vector3 movePos;
    private Vector3 moveDir;
    private EnemyStateMachine enemy;
    private float timer;

    private void Awake()
    {
        enemy = GetComponent<EnemyStateMachine>();
    }

    private void Start()
    {
        GetNewRandomPosition();
        timer = timeToIdle;
    }
    public override void Act()
    {
        moveDir = (movePos - transform.position).normalized;
        transform.Translate(moveDir * (wanderSpeed * Time.deltaTime));
        timer -= Time.deltaTime;

        if (CanGetNewPosition() && timer >0f)
        {
            GetNewRandomPosition();
        }
        else if (timer < 0f)
        {
            Debug.Log("idle");
            transform.Translate(Vector3.zero);
        }
        if (timer <= -2f)
        {
            Debug.Log("wander");
            timer = timeToIdle;
        }
       
            
        
    }

    // enemy can use random position or availible tile position to move. 
    private void GetNewRandomPosition()
    {
        if (useRandomWander)
            movePos = transform.position + GetRandomDirection();

        if (useTilesPosition)
        {
            movePos =  enemy.CurrentRoom.GetTilePosition();
        }
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
