using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Entity
{
    public static event Action OnPlayerDeath;
    public Playerinput input { get; private set; }

    public PlayeridleState idlestate { get; private set; }
    public PlayerMovestate movestate { get; private set; }
    public Player_JumpState jumpstate { get; private set; }
    public Player_FallState fallstate { get; private set; }
    public Player_WallSlideState wallslidestate { get; private set; }
    public Player_WallJumpState walljumpstate { get; private set; }

    public PlayerDashState dashState { get; private set; }

    public PlayerBasicAttackState basicAttackState { get; private set; }
    public Player_JumpAttackState jumpAttackState { get; private set; }

    public Player_DeadState deadState { get; private set; }

    public PlayerCounterAttackState counterAttackState { get; private set; }


    [Header("Attack details")]
    public Vector2[] attackVelocity;
    public Vector2 jumpAttackVelocity;
    public float attackvelocityDuration = .1f;
    public float comboResetTime = 1;
    private Coroutine queuedAttackCo;


    [Header("Movement details")]
    public float moveSpeed;
    public float jumpForce = 5;
    public Vector2 walljumpForce;

    [Range(0, 1)]
    public float inAirMoveMultiplier = 0.7f;
    [Range(0, 1)]
    public float wallSlideSlowMultiplier = 0.7f;
    [Space]
    public float dashDuration = .25f;
    public float dashSpeed = 20;


    public Vector2 moveinput { get; private set; }

    protected override void Awake()
    {
        base.Awake(); input = new Playerinput();
        idlestate = new PlayeridleState(this, stateMachine, "idle");
        movestate = new PlayerMovestate(this, stateMachine, "move");
        jumpstate = new Player_JumpState(this, stateMachine, "jumpFall");
        fallstate = new Player_FallState(this, stateMachine, "jumpFall");
        wallslidestate = new Player_WallSlideState(this, stateMachine, "wallSlide");
        walljumpstate = new Player_WallJumpState(this, stateMachine, "wallFall");
        dashState = new PlayerDashState(this, stateMachine, "dash");
        basicAttackState = new PlayerBasicAttackState(this, stateMachine, "basicAttack");
        jumpAttackState = new Player_JumpAttackState(this, stateMachine, "jumpAttack");
        deadState = new Player_DeadState(this, stateMachine, "dead");
        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "counterAttack");

    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Intialize(idlestate);
    }

    protected override IEnumerator SlowDownEntityCo(float duration, float slowMultiplier)
    {
        float originalMoveSpeed = moveSpeed;
        float originalJumpForce = jumpForce;
        float originalAnimSpeed = anim.speed;
        Vector2 originalWallJump = walljumpForce;
        Vector2 originalJumpAttack = jumpAttackVelocity;
        Vector2[] originalAttackVelocity = new Vector2 [ attackVelocity.Length];
        Array.Copy(attackVelocity, originalAttackVelocity, attackVelocity.Length);

        float speedMultiplier = 1 - slowMultiplier;

        moveSpeed = moveSpeed * speedMultiplier;
        jumpForce = jumpForce * speedMultiplier;
        anim.speed = anim.speed * speedMultiplier;
        walljumpForce = walljumpForce * speedMultiplier;
        jumpAttackVelocity = jumpAttackVelocity * speedMultiplier;

        for(int i =0; i<attackVelocity.Length; i++)
        {
            attackVelocity[i] = attackVelocity[i] * speedMultiplier;
        }

        yield return new WaitForSeconds(duration);


        moveSpeed = originalMoveSpeed;
        jumpForce = originalJumpForce;
        anim.speed = originalAnimSpeed;
        walljumpForce = originalWallJump;
        jumpAttackVelocity = originalJumpAttack;

        for (int i = 0; i < attackVelocity.Length; i++)
        {
            attackVelocity[i] = originalAttackVelocity[i];
        }

    }

    public override void EntityDeath()
    {
        base.EntityDeath();
        OnPlayerDeath?.Invoke();
        stateMachine.changeState(deadState);
    }

    public void EnterAttackStateWithDelay()
    {
        if (queuedAttackCo != null)
            StopCoroutine(queuedAttackCo);

        queuedAttackCo = StartCoroutine(EnterAttackStateWithDelayCo());
    }


    private IEnumerator EnterAttackStateWithDelayCo()
    {
        yield return new WaitForEndOfFrame();
        stateMachine.changeState(basicAttackState);
    }
    private void OnEnable()
    {
        input.Enable();

        //input.Player.movement.started;ÊäÈë¿ªÊ¼
        input.Player.movement.performed += ctx => moveinput = ctx.ReadValue<Vector2>();

        input.Player.movement.canceled += ctx => moveinput = Vector2.zero;
    }

    private void OnDisable()
    {
        input.Disable();
    }

   

}
