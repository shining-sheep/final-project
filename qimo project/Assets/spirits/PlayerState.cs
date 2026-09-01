using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState :EntityState
{   protected Player player;
    protected Playerinput input;
    public PlayerState(Player player ,StateMachine stateMachine,string animBoolName) : base(stateMachine,animBoolName)
    {   
        this.player = player;
        anim = player.anim;
        rb = player.rb;
        input = player.input;
       
    }

    public override void Update()
    {
        base.Update();
    
    anim.SetFloat("yVelocity", rb.velocity.y);

    if (input.Player.Dash.WasPressedThisFrame() && CanDash())
    StateMachine.changeState(player.dashState);
     }

    private bool CanDash()
    {
        if (player.wallDetected)
            return false;
        if (StateMachine.currentState == player.dashState)
            return false;

        return true;
    }
}
