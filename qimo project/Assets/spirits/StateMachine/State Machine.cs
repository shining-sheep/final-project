using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    public EntityState currentState { get; private set; }
    public bool canChangeState;

    public void Intialize(EntityState startState)
    {
        canChangeState = true;
        currentState = startState;
        currentState.Enter();
       
    }
    public void changeState(EntityState newState)
    {
        if (canChangeState == false)
            return;

        currentState.Exit();
        currentState = newState;
        currentState.Enter();

    }

    public void UpdateAciveState()
    {
        currentState.Update();
    }

    public void SwitchOffStateMachine() => canChangeState = false;
}
