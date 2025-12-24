using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{
    protected EnemyController enemyBrain;
    protected EnemyConfig _data;

    private void Awake()
    {
        if (enemyBrain == null)
        {
            enemyBrain = GetComponentInParent<EnemyController>();
            if (enemyBrain == null)
                Debug.LogWarning($"AIDecision on '{gameObject.name}' could not find EnemyController in parents.");
            else
                _data = enemyBrain.EnemyConfig;
        }
    }

    public virtual void Init(EnemyController brain)
    {
        enemyBrain = brain;
        _data = brain != null ? brain.EnemyConfig : null;
        if (brain == null)
            Debug.LogWarning($"AIDecision.Init: brain is null for decision '{name}'");
    }

    public abstract bool MakeADecision();
}
