using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{
    protected EnemyController enemyBrain;
    protected EnemyConfig _data => enemyBrain != null ? enemyBrain.EnemyConfig : null;

    private void Awake()
    {
        // fallback - try to find brain in parents if Init was not called
        if (enemyBrain == null)
        {
            enemyBrain = GetComponentInParent<EnemyController>();
            if (enemyBrain == null)
                Debug.LogWarning($"AIAction on '{gameObject.name}' could not find EnemyController in parents.");
        }
    }

    // Optional one-time initialization to inject dependencies
    public virtual void Init(EnemyController brain)
    {
        enemyBrain = brain;
        if (enemyBrain == null)
            Debug.LogWarning($"AIAction.Init: brain is null for action '{name}'");
    }

    // Called once when the state is entered
    public virtual void OnEnter() { }

    // Called each update while the state is active
    public virtual void OnUpdate() { }

    // Called once when the state is exited
    public virtual void OnExit() { }
}
