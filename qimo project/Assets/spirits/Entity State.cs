using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityState 
{
    protected StateMachine StateMachine;
    protected string stateName;

    public EntityState(StateMachine stateMachine,string stateName) 
    {
        this.StateMachine = stateMachine;
        this.stateName = stateName;
       
    }

    public virtual void Enter()//状态机进入新状态
    {

    }

    public virtual void Update()//状态更新
    {

    }
    public virtual void Exit()//状态退出
    {

    }
}
