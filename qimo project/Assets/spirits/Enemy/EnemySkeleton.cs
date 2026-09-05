using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : Enemy,ICounterable
{
    protected override void Awake(){
        base.Awake();
        idleState = new EnemyIdleState(this, stateMachine, "idle");
        moveState = new EnemyMoveState(this, stateMachine, "move");
        attackState = new EnemyAttackState(this, stateMachine,"attack");
        battleState = new EnemyBattleState(this, stateMachine, "battle");
        deadState = new Enemy_DeadState(this, stateMachine, "idle");
        stunnedState = new EnemyStunnedState(this, stateMachine, "stunned");

    }
    protected override void Start()
    {
        base.Start();

        stateMachine.Intialize(idleState);
    }
    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.F))
            HandleCounter();

    }
    public void HandleCounter()
    {
        if (canBeStunned == false)
            return;

        stateMachine.changeState(stunnedState);
    }

}
