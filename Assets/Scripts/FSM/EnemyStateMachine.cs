using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private string initialStateName;

    [Header("States")]
    [SerializeField] private FSMState[] states;
    
    public FSMState CurrentState { get; set; }

    private void Start()
    {
        ChangeState(initialStateName);
    }
    private void Update()
    {
        CurrentState.ExcuteState(this);
    }
    public void ChangeState(string newStateName)
    {
        FSMState newstate = GetState(newStateName);
        if (newstate != null)
        {
            CurrentState = newstate;
        }
        else return;
    }

    private FSMState GetState(string newStateName)
    {
        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].stateName == newStateName)
                return states[i];
        }
        return null;
    }
}
