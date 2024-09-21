using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingState : State
{
    protected BuildingController controller;
    public BuildingState(StateMachine _stateMachine, BuildingController controller) : base(_stateMachine)
    {
        this.controller = controller;
    }
}
