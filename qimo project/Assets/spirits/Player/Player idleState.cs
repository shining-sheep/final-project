using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeridleState : EntityState
{
    public PlayeridleState(Player player,StateMachine stateMachine, string stateName) : base(player,stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.moveinput.x != 0)
            StateMachine.changeState(player.movestate);
        

       

    }
}
