using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    private Playerinput input;
    private StateMachine stateMachine;
    public PlayeridleState idlestate { get; private set; }
    public PlayerMovestate movestate { get; private set; }

    public Vector2 moveinput { get; private set; }

    [Header("Movement details")]
    public float moveSpeed;


    private bool facingRight = true;



    


    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();
        input = new Playerinput();
        idlestate = new PlayeridleState(this,stateMachine, "idle");
        movestate = new PlayerMovestate(this,stateMachine, "move");

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
        stateMachine.UpdateAciveState();
    }


    public void SetVelocity(float xVelocity,float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
    }

    private void HandleFlip()
    {

    }


    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }
}
