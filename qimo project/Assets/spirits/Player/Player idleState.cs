using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeridleState : PlayerGroundedState
{
    public PlayeridleState(Player player,StateMachine stateMachine, string stateName) : base(player,stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (player.moveinput.x == player.facingDir && player.wallDetected)
            return;

        if (player.moveinput.x != 0)
            StateMachine.changeState(player.movestate);


        
        

       

    }
}
