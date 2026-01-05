using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectDecision : AIDecision
{
    private Transform target;
    public override bool MakeADecision()
    {
        return DetectPlayerInRange();
    }

    public bool DetectPlayerInRange()
    {
        Collider2D hit = Physics2D.OverlapCircle(enemyBrain.transform.position, _data.rangeCanDetectPlayer, _data.playerLayer);
        if (hit != null)
        {
            target = hit.transform;
            return DetectObstace();
        }
        target = null;
        enemyBrain.Player = null;
        return false;
    }

    // detect obstace which is between enemy and player;
    public bool DetectObstace()
    {
        if ((target.transform.position - enemyBrain.transform.position).magnitude > _data.rangeCanDetectPlayer)
        {
            return false;
        }
        Vector3 direction = (target.transform.position - enemyBrain.transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(enemyBrain.transform.position, direction, _data.rangeCanDetectPlayer, _data.obstacleLayer);
        if (hit.collider == null)
        {
            enemyBrain.Player = target;
            return true;
        }
        else
        {
            enemyBrain.Player = null;
            return false;
        }
    }

}
