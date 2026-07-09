using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public Playerinput input { get; private set; }
    private StateMachine stateMachine;
    public PlayeridleState idlestate { get; private set; }
    public PlayerMovestate movestate { get; private set; }
    public Player_JumpState jumpstate { get; private set; }
    public Player_FallState fallstate { get; private set; }
    public Player_WallSlideState wallslidestate { get; private set; }
    public Player_WallJumpState walljumpstate { get; private set; }

    public PlayerDashState dashState { get; private set; } 

    public PlayerBasicAttackState basicAttackState { get; private set; }
    public Player_JumpAttackState jumpAttackState { get; private set; }

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




    private bool facingRight = true;
    public  int facingDir { get; private set; } = 1;

    public Vector2 moveinput { get; private set; }

    [Header("Collision detection")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask  whatIsGround;
    public bool groundDetected { get; private set; }
    public bool wallDetected { get; private set; }








    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();
        input = new Playerinput();
        idlestate = new PlayeridleState(this,stateMachine, "idle");
        movestate = new PlayerMovestate(this, stateMachine, "move");
        jumpstate = new Player_JumpState(this,stateMachine, "jumpFall");
        fallstate = new Player_FallState(this, stateMachine, "jumpFall");
        wallslidestate = new Player_WallSlideState(this, stateMachine, "wallSlide");
        walljumpstate = new Player_WallJumpState(this, stateMachine, "wallFall");
        dashState = new PlayerDashState(this, stateMachine, "dash");
        basicAttackState = new PlayerBasicAttackState(this, stateMachine, "basicAttack");
        jumpAttackState = new Player_JumpAttackState(this, stateMachine, "jumpAttack");


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

    private void Start()
    {

        stateMachine.Intialize(idlestate);
    }

    private void Update()
    {
        HandleCollisionDetection();
        stateMachine.UpdateAciveState();
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

    public void CallAnimationTrigger()
    {
        stateMachine.currentState.CallAnimationTrigger();
    }


    public void SetVelocity(float xVelocity,float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    private void HandleFlip(float xVelocity)
    {
        if (xVelocity > 0 && facingRight == false)
            Flip();
        else if (xVelocity < 0 && facingRight)
            Flip();

    }



   public void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        facingDir = facingDir * -1;
    }


    private void HandleCollisionDetection()
    {
        groundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
       wallDetected = Physics2D.Raycast(transform.position, Vector2.right*facingDir, wallCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(wallCheckDistance*facingDir,0));
    }
}
