using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInSightDecision : FSMDecision
{
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float distanceToPlayer;

    private EnemyStateMachine enemy;
    private void Awake()
    {
        enemy = GetComponent<EnemyStateMachine>();
    }
    public override bool Decision()
    {
        return DecisionPlayerInSight();
    }

    public bool DecisionPlayerInSight()
    {
        if (enemy.Player == null) return false;
        Vector3 dir = enemy.Player.position - transform.position;
        RaycastHit2D detect = Physics2D.Raycast(transform.position, dir.normalized
            , distanceToPlayer, obstacleLayer);

        // neu phat hien va cham voi obstacles
        if (detect.collider != null)
        {
            return false;
        }
        return true;
    }

    private void OnDrawGizmosSelected()
    {
        if(enemy.Player == null) return;
        Gizmos.color = DecisionPlayerInSight() ? Color.green : Color.black; 
        Gizmos.DrawLine(transform.position, enemy.Player.position);
    }
}
