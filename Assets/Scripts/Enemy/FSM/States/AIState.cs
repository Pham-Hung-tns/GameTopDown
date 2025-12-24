using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    private EnemyController enemyBrain = null; 

    [SerializeField]
    private List<AIAction> actions = null;

    [SerializeField]
    private List<AITransition> transitions = null;

    private bool isActive = false;

    private void Awake()
    {
        // Use GetComponentInParent so the state can be on child objects and still find the enemy controller
        enemyBrain = GetComponentInParent<EnemyController>();
        if (enemyBrain == null)
            Debug.LogWarning($"AIState on '{gameObject.name}' could not find NormalEnemyStates in parents.");
    }

    public void Initialize(EnemyController _brain)
    {
        enemyBrain = _brain;
    }

    public void EnterState()
    {
        isActive = true;

        // Initialize actions and decisions with the brain reference to avoid GetComponent lookups
        if (enemyBrain == null)
            enemyBrain = GetComponentInParent<EnemyController>();

        if (actions != null)
        {
            foreach (var action in actions)
            {
                if (action == null) continue;
                action.Init(enemyBrain);
                action.OnEnter();
            }
        }

        if (transitions != null)
        {
            foreach (var t in transitions)
            {
                if (t == null || t.Decisions == null) continue;
                foreach (var d in t.Decisions)
                {
                    if (d == null) continue;
                    d.Init(enemyBrain);
                }
            }
        }
    }

    public void ExitState()
    {
        isActive = false;
        // Call OnExit for all actions
        if (actions != null)
        {
            foreach (var action in actions)
            {
                if (action == null) continue;
                action.OnExit();
            }
        }
    }

    public void UpdateState()
    {
        if (isActive == false) return;

        if (actions != null)
        {
            foreach (var action in actions)
            {
                if (action == null)
                    continue;
                action.OnUpdate();
            }
        }

        if (transitions != null)
        {
            foreach (var transition in transitions)
            {
                if (transition == null)
                    continue;

                bool result = false;

                var decisions = transition.Decisions;
                if (decisions == null)
                    continue;

                foreach (var decision in decisions)
                {
                    if (decision == null)
                    {
                        result = false;
                        break;
                    }

                    result = decision.MakeADecision();
                    if (result == false)
                        break;
                }

                if (result)
                {
                    if (transition.PositiveResult != null)
                    {
                        if (enemyBrain == null)
                        {
                            Debug.LogWarning("AIState: cannot change state because enemyBrain is null");
                            return;
                        }

                        enemyBrain.ChangeToState(transition.PositiveResult);
                        return;
                    }
                }
                else
                {
                    if (transition.NegativeResult != null)
                    {
                        if (enemyBrain == null)
                        {
                            Debug.LogWarning("AIState: cannot change state because enemyBrain is null");
                            return;
                        }

                        enemyBrain.ChangeToState(transition.NegativeResult);
                        return;
                    }
                }
            }
        }
    }
}
