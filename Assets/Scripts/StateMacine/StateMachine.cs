using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    //When creating a new state machine dont forget to add a variable to access the stats of the controlled script
    public State CurrentState { get; set; }

    public void Initialize(State initialState)
    {
        CurrentState = initialState;
        CurrentState.EnterState();
    }

    public void ChangeState(State newState) 
    { 
        CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }

}
