using System;

[Serializable]
public class FSMState
{
    public string stateName;
    public FSMAction[] actions;
    public FSMTransition[] transitions;

    public void ExcuteState(EnemyStateMachine enemy)
    {
        ExcuteActions();
        ExcuteTransitions(enemy);
    }

    public void ExcuteActions()
    {
        if(actions.Length <= 0) { return; }
        for(int i =0; i<actions.Length; i++)
        {
            actions[i].Act();
        }
    }

    public void ExcuteTransitions(EnemyStateMachine enemy)
    {
        if(transitions.Length <= 0) { return; }
        for(int i = 0; i<transitions.Length; i++)
        {
            bool value = transitions[i].Decision.Decision();
            if (value)
            {
                if (string.IsNullOrEmpty(transitions[i].TrueState) == false)
                {
                    enemy.ChangeState(transitions[i].TrueState);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(transitions[i].FalseState) == false)
                {
                    enemy.ChangeState(transitions[i].FalseState);
                }
            }
        }
    }

}

