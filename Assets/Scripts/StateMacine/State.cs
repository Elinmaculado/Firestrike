using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State 
{
    //When creating a new state machine dont forget to add a variable to access the stats of the controlled script
    protected StateMachine stateMachine;

    public State(StateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
    }
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void Update() { }
    public virtual void PhysicsUpdate() { }
}
