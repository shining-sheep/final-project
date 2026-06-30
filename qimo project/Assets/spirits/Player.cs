using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private StateMachine stateMachine;

    private EntityState idleState;


    private void Awake()
    {
        stateMachine = new StateMachine();
        idleState = new EntityState(stateMachine, "Idle state");
    }
    private void Start()
    {
        stateMachine.Intialize(idleState);
    }
    private void Update()
    {
        stateMachine.currentState.Update();
    }
}
