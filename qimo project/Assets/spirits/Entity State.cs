using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityState 
{
    protected StateMachine StateMachine;
    protected string animBoolName;
    protected Player player;

    protected Animator anim;
    protected Rigidbody2D rb;
    protected Playerinput input;

    protected float stateTimer;

    public EntityState(Player player ,StateMachine stateMachine,string animBoolName) 
    {   
        this.player = player;
        this.StateMachine = stateMachine;
        this.animBoolName = animBoolName;


        anim = player.anim;
        rb = player.rb;
        input = player.input;
       
    }

    public virtual void Enter()//状态机进入新状态
    {
       anim.SetBool(animBoolName, true);
    }

    public virtual void Update()//状态更新
    {
        stateTimer -= Time.deltaTime;
        anim.SetFloat("yVelocity", rb.velocity.y);

        if (input.Player.Dash.WasPressedThisFrame() && CanDash())
            StateMachine.changeState(player.dashState);
    }
    public virtual void Exit()//状态退出
    {
        anim.SetBool(animBoolName, false);
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
