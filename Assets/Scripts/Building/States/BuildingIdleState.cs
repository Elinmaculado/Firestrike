using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BuildingIdleState : BuildingState
{
    public BuildingIdleState(StateMachine _stateMachine, BuildingController stats) : base(_stateMachine, stats)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        controller.fireTimer = 0;
        controller.isOnFire = false;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Update()
    {
        base.Update();
        if (controller.fireTimer > 0)
        {
            controller.fireTimer -= Time.deltaTime;
        }
        if (controller.fireTimer > controller.fireTime)
        {
            stateMachine.ChangeState(controller.burningState);
        }
    }
}
