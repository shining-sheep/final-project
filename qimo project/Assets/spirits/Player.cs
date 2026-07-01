using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System;

public class Player : MonoBehaviour
{
    private StateMachine stateMachine;

    private EntityState idleState;

    private Animator anim;
    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 3.5f;//移动速度
    [SerializeField] private float jumpForce = 8;   //跳跃高度
    private float xInput;

    [SerializeField] private bool facingRight = true; //面向右边
    private void Awake()
    {

        stateMachine = new StateMachine();
        idleState = new EntityState(stateMachine, "Idle state");

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        stateMachine.Intialize(idleState);

        HandleInput();
        HandleMovement();
        HandleAnimations();
        HandleFlip();
    }
    private void Update()
    {
        stateMachine.currentState.Update();
    }

    private void HandleAnimations()                                               //动画控制
    {
        bool isMoving = rb.velocity.x != 0;

        anim.SetBool("isMoving", isMoving);
    }
    private void HandleInput()                                                    //按键控制
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void HandleMovement()                                                 //移动控制
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
    }

    private void HandleFlip()                                                     //转向控制
    {
        if(rb.velocity.x > 0 && facingRight == false)
        {
            Flip();
        }
        else if (rb.velocity.x < 0 && facingRight == true)
        {
            Flip();
        }
    }
    private void Jump()                                                           //跳跃
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    [ContextMenu("Flip")]
    private void Flip()                                                            //转向
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;

    }
}
