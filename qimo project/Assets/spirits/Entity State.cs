using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityState 
{
    protected StateMachine StateMachine;
    protected string animBoolName;
    protected Player player;

    protected Animator anim;

    public EntityState(Player player ,StateMachine stateMachine,string animBoolName) 
    {   
        this.player = player;
        this.StateMachine = stateMachine;
        this.animBoolName = animBoolName;
        anim = player.anim;

       
    }

    public virtual void Enter()//状态机进入新状态
    {
       anim.SetBool(animBoolName, true);
    }

    public virtual void Update()//状态更新
    {
        Debug.Log("shangchuan" + animBoolName);
    }
    public virtual void Exit()//状态退出
    {
        anim.SetBool(animBoolName, false);
    }
}
