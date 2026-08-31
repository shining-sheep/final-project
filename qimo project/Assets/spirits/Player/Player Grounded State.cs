using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (rb.velocity.y < 0 && player.groundDetected == false)
            StateMachine.changeState(player.fallstate);

        if (Input.GetButtonDown("Jump"))
            StateMachine.changeState(player.jumpstate);
        if (input.Player.Attack.WasPerformedThisFrame())
            StateMachine.changeState(player.basicAttackState);
    }
}
