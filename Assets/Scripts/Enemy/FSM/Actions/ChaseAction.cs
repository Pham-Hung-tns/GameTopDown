using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChaseAction : AIAction
{
    private Transform player;
    private EnemyMovement movement;
    private float lastRequest = -999f;

    public override void OnEnter()
    {
        // initialize chase
        player = enemyBrain.Player?.transform;
        movement = enemyBrain != null ? enemyBrain.GetComponent<EnemyMovement>() : null;
        if (player != null && movement != null)
            movement.RequestPath(player.position);
        enemyBrain.ChangeAnim(Settings.WANDER_STATE);
    }

    public override void OnUpdate()
    {
        if (player == null || enemyBrain == null)
            return;

        // periodically request a new path if player moved
        if (movement != null && Time.time - lastRequest > 0.25f)
        {
            movement.RequestPath(player.position);
            enemyBrain.PatrolPosition = player.position;
            lastRequest = Time.time;
        }
    }

    public override void OnExit()
    {
        enemyBrain.Player = null;
    }
}
