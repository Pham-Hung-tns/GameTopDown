using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAction : AIAction
{
    private Vector3 targetPosition;

    public override void OnEnter()
    {
        // choose a single random target when entering the wander state
        targetPosition = GetRandomDirection();
        enemyBrain.ChangeAnim(Settings.WANDER_STATE);
        enemyBrain.TimeLimit = enemyBrain.EnemyConfig.timeToWander;
    }

    public override void OnUpdate()
    {
        // move towards the chosen target
        if (enemyBrain == null || enemyBrain.Rb == null || enemyBrain.EnemyConfig == null)
            return;
        var rbPos = enemyBrain.Rb.position;
        var tgt = (Vector2)targetPosition;
        float step = _data.speed * Time.deltaTime;
        Vector2 next = Vector2.MoveTowards(rbPos, tgt, step);
        enemyBrain.ChangeDirection(targetPosition);
        enemyBrain.Rb.MovePosition(next);
    }

    public override void OnExit()
    {
        //enemyBrain.ChangeAnim(EnemyController.IDLE_STATE);
        enemyBrain.Rb.velocity = Vector2.zero;
    }

    public Vector3 GetRandomDirection()
    {
        float xPosition = Random.Range(-_data.moveRange.x, _data.moveRange.x);
        float yPosition = Random.Range(-_data.moveRange.y, _data.moveRange.y);
        enemyBrain.PatrolPosition = new Vector3(xPosition, yPosition);
        return enemyBrain.PatrolPosition;
    }
}
