using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    public EntityState currentState { get; private set; }

    public void Intialize(EntityState startState)
    {
        currentState = startState;
        currentState.Enter();
       
    }
    public void changeState(EntityState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();

    }
}
