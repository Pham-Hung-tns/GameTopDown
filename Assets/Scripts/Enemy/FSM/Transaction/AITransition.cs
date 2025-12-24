using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    [field: SerializeField]
    public List<AIDecision> Decisions { get; set; }

    [field: SerializeField]
    public AIState PositiveResult { get; set; }

    [field: SerializeField]
    public AIState NegativeResult { get; set; }

    private void Awake()
    {
        // Auto-fill decisions only if user hasn't assigned any in the inspector
        if (Decisions == null || Decisions.Count == 0)
        {
            // Prefer child decisions so users can compose under this transition
            Decisions = new List<AIDecision>(GetComponentsInChildren<AIDecision>());
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        // Warn if no decisions assigned
        if (Decisions == null || Decisions.Count == 0)
        {
            Debug.LogWarning($"AITransition on '{gameObject.name}' has no Decisions assigned. Add AIDecision components or set Decisions in the Inspector.", this);
        }

        // Warn if both results are null - transition will do nothing
        if (PositiveResult == null && NegativeResult == null)
        {
            Debug.LogWarning($"AITransition on '{gameObject.name}' has no PositiveResult or NegativeResult assigned; transition will not change states.", this);
        }
    }
#endif
}
