using UnityEngine;

public class BuildingIdleState : BuildingState
{
    public BuildingIdleState(StateMachine _stateMachine, BuildingController controller) : base(_stateMachine, controller)
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
        if (controller.fireTimer >= controller.fireTime)
        {
            stateMachine.ChangeState(controller.burningState);
        }
        controller.burningSprite.transform.localScale = Vector3.one *  controller.fireTimer;
    }
}
