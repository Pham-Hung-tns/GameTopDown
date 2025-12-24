using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    public override void OnEnter()
    {
        // setup attack (e.g. reset timers)
        enemyBrain.TimeLimit = enemyBrain.EnemyConfig.timeToAttack;
    }

    public override void OnUpdate()
    {
        // attack behavior while in attack state
    }

    public override void OnExit()
    {
        // cleanup
    }
}
