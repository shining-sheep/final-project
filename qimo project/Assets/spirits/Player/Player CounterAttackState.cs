using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    private PlayerCombat combat;
    private bool counteredSombody;
    public PlayerCounterAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        combat = player.GetComponent<PlayerCombat>();
    }
    public override void Enter()
    {
        base.Enter();

        stateTimer = combat.GetCounterRecoveryDuration();
        counteredSombody = combat.CounterAttackPerformed();
        anim.SetBool("counterAttackPerformed", counteredSombody);
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(0, rb.velocity.y);

        if(triggerCalled)
            StateMachine.changeState(player.idlestate);

        if (stateTimer < 0 && counteredSombody == false)
            StateMachine.changeState(player.idlestate);
    }

}
