using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAttackState : EntityState
{
    private float attackVelocityTimer;
    public PlayerBasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

       
        GenerateAttackVelocity();

    }
    public override void Update()
    {
        base.Update();
        if (triggerCalled)
            StateMachine.changeState(player.idlestate);
    }
    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;
        if (attackVelocityTimer < 0)
            player.SetVelocity(0, rb.velocity.y);
          
    }

    private void GenerateAttackVelocity()
    {
        attackVelocityTimer = player.attackvelocityDuration;
        player.SetVelocity(player.attackVelocity.x * player.facingDir, player.attackVelocity.y);
    }


}
