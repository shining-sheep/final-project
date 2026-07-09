using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AiredState : EntityState
{
    public Player_AiredState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        float horizontal = player.moveinput.x * player.moveSpeed * player.inAirMoveMultiplier;
        float vertical = player.rb.velocity.y;   // 注意用 player.rb 访问刚体

        player.SetVelocity(horizontal, vertical);

        if (input.Player.Attack.WasPerformedThisFrame())
            StateMachine.changeState(player.jumpAttackState);
    }
}
