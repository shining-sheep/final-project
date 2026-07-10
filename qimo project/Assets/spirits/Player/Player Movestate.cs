using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovestate : PlayerGroundedState
{
    public PlayerMovestate(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.moveinput.x == 0 || player.wallDetected)
            StateMachine.changeState(player.idlestate);


        player.SetVelocity(player.moveinput.x * player.moveSpeed,rb.velocity.y);

    }
}
