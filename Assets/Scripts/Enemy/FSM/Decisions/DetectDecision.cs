using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectDecision : AIDecision
{
    private GameObject player;
    public Transform detectpos;
    public override bool MakeADecision()
    {
        return DetectPlayerInRange();
    }

    public bool DetectPlayerInRange()
    {
        Collider2D hit = Physics2D.OverlapCircle(detectpos.position, _data.rangeCanDetectPlayer, _data.playerLayer);
        if (hit != null)
        {
            player.gameObject.transform.position = hit.transform.position;
            return DetectObstace();
        }
        player = null;
        return false;
    }

    // detect obstace which is between enemy and player;
    public bool DetectObstace()
    {
        if ((player.transform.position - detectpos.position).magnitude > _data.rangeCanDetectPlayer)
        {
            return false;
        }
        Vector3 direction = (player.transform.position - detectpos.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(detectpos.position, direction, _data.rangeCanDetectPlayer, _data.obstacleLayer);
        if (hit.collider == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Gizmos
    public virtual void OnDrawGizmos()
    {
        //Detect Player
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(detectpos.position, _data.rangeCanDetectPlayer);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(detectpos.position, Vector2.down * _data.rangeCanDetectPlayer);
    }
}
