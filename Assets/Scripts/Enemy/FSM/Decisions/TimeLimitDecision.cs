using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLimitDecision : AIDecision
{
    public override bool MakeADecision()
    {
        return enemyBrain.CurrentTime > enemyBrain.TimeLimit;
    }
}
