using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player_WallSlideState : PlayerState
{
    public Player_WallSlideState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        HandleWallSlide();

      if (Input.GetButtonDown("Jump"))
            StateMachine.changeState(player.jumpstate);

        if (player.wallDetected == false)
            StateMachine.changeState(player.fallstate);

        if (player.groundDetected)
        {
            StateMachine.changeState(player.idlestate);
            player.Flip();
        }
    }

    private void HandleWallSlide()
    {
        if (player.moveinput.y < 0)
            player.SetVelocity(player.moveinput.x, rb.velocity.y);
        else
            player.SetVelocity(player.moveinput.x, rb.velocity.y * player.wallSlideSlowMultiplier);
    }
}