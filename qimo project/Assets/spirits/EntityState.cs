using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class EntityState 
{
    protected StateMachine StateMachine;
    protected string animBoolName;


    protected Animator anim;
    protected Rigidbody2D rb;


    protected float stateTimer;
    protected bool triggerCalled;

    public EntityState(StateMachine stateMachine, string animBoolName)
    {
        this.StateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }
    public virtual void Enter()//状态机进入新状态
    {
        anim.SetBool(animBoolName, true);
        triggerCalled = false;
    }

    public virtual void Update()//状态更新
    {
        stateTimer -= Time.deltaTime;
    }
    public virtual void Exit()//状态退出
    {
        anim.SetBool(animBoolName, false);
    }
    public void CallAnimationTrigger()
    {
        triggerCalled = true;
    }
}
