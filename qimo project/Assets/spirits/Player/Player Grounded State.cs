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

        if (input.Player.Jump.WasPressedThisFrame())
            StateMachine.changeState(player.jumpstate);

        if (input.Player.Attack.WasPressedThisFrame())
            StateMachine.changeState(player.basicAttackState);

        if (input.Player.CounterAttack.WasPressedThisFrame())
            StateMachine.changeState(player.counterAttackState);
    }
}
