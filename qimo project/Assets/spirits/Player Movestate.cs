using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovestate : EntityState
{
    public PlayerMovestate(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();
        if (player.moveinput.x == 0)
            StateMachine.changeState(player.idlestate);

    }
}
