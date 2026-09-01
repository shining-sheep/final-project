using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : Enemy
{
   
   protected override void Awake(){
        base.Awake();
        idleState = new EnemyIdleState(this, stateMachine, "idle");
        moveState = new EnemyMoveState(this, stateMachine, "move");

    }
    protected override void Start()
    {
        base.Start();

        stateMachine.Intialize(idleState);
    }
}
