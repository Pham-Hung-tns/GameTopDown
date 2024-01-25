using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerAction : FSMAction
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float rangeDetect;
    private EnemyStateMachine enemy;

    private void Awake()
    {
        enemy = GetComponent<EnemyStateMachine>();
    }
    public override void Act()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, rangeDetect, playerLayer);
        if(collider != null )
        {
            enemy.Player = collider.transform;
        }
        else
        {
            enemy.Player = null;
            return;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangeDetect);
    }
}
