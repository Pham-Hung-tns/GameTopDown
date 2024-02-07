using UnityEngine;

public class State
{
    protected EnemyStateMachine enemy;
    public State(EnemyStateMachine enemy)
    {
        this.enemy = enemy;
        
    }
    public virtual void OnEnter()
    {

    }
    public virtual void OnUpdate()
    {

    }
    public  virtual void OnExit()
    {

    }
}

