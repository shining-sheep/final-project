using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAttackState : PlayerState
{
    private float attackVelocityTimer;
    private float lastTimeAttacked;
    private bool comboAttackQueued;
    private const int FirstComboIndex = 1;//初始攻击计数变量
    private int attackDir;
    private int comboIndex = 1;
    private int comboLimit = 3;

    
    public PlayerBasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        if (comboLimit != player.attackVelocity.Length)
            comboLimit = player.attackVelocity.Length;
    }

    public override void Enter()
    {
        base.Enter();
        comboAttackQueued = false;
        ResetComboIndexIfNeeded();

        attackDir = player.moveinput.x != 0 ? ((int)player.moveinput.x) : player.facingDir;

        anim.SetInteger("basicAttackindex", comboIndex);
        ApplyAttackVelocity();

    }

    

    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        if (input.Player.Attack.WasPerformedThisFrame())
            QueueNextAttack();

        if (triggerCalled)
            HandletateExit();



    }
    public override void Exit()
    {
        base.Exit();
        comboIndex++;
        lastTimeAttacked = Time.time;
    }
     
    private void HandletateExit()
    {
        if (comboAttackQueued)
        {
            anim.SetBool(animBoolName, false);
            player.EnterAttackStateWithDelay();
        }


        else
            StateMachine.changeState(player.idlestate);
    }

    private void QueueNextAttack()
    {
        if (comboIndex < comboLimit)
            comboAttackQueued = true;
    }
    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;
        if (attackVelocityTimer < 0)
            player.SetVelocity(0, rb.velocity.y);
          
    }

    private void ApplyAttackVelocity()
    {
        Vector2 attackVelocity = player.attackVelocity[comboIndex-1];
        attackVelocityTimer = player.attackvelocityDuration;
        player.SetVelocity(attackVelocity.x * attackDir, attackVelocity.y);
    }

private void ResetComboIndexIfNeeded()
    {
        if (Time.time > lastTimeAttacked + player.comboResetTime)
            comboIndex = FirstComboIndex;

        if (comboIndex > comboLimit||Time.time > lastTimeAttacked + player.comboResetTime)
            comboIndex = FirstComboIndex;
    }
}
