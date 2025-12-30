using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    private Transform player;
    private EnemyMovement movement;
    private float lastRequest = -999f;

    public override void OnEnter()
    {
        // initialize chase
        player = GameObject.FindWithTag(Settings.playerTag)?.transform;
        movement = enemyBrain != null ? enemyBrain.GetComponent<EnemyMovement>() : null;
        if (player != null && movement != null)
            movement.RequestPath(player.position);
        enemyBrain.ChangeAnim(Settings.ATTACK_STATE);
    }

    public override void OnUpdate()
    {
        if (player == null)
            player = GameObject.FindWithTag(Settings.playerTag)?.transform;
        if (player == null || enemyBrain == null)
            return;

        // periodically request a new path if player moved
        if (movement != null && Time.time - lastRequest > 0.25f)
        {
            movement.RequestPath(player.position);
            lastRequest = Time.time;
        }
    }

    public override void OnExit()
    {
        // cleanup
    }
}
