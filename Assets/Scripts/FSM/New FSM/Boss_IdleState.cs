
using UnityEngine;

public class Boss_IdleState : State
{
    BossStates boss;
    BossConfig bossConfig;
    public Boss_IdleState(EnemyStateMachine enemy, BossConfig bossConfig, BossStates boss) : base(enemy)
    {
        this.boss = boss;
        this.bossConfig = bossConfig;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        boss.ChangeAnim(EnemyStateMachine.IDLE_STATE);
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        if (boss.DetectPlayerInRange())
        {
            boss.ChangeState(boss.attackRandomState);
        }
        //timer -= Time.deltaTime;
        //if(timer <= 0)
        //{
        //}
    }
}

