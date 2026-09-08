using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        SyncAttackSpeed();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
            StateMachine.changeState (enemy.battleState);
    }
}
