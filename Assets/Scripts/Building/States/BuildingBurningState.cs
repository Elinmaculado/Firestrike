using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BuildingBurningState : BuildingState
{


    public BuildingBurningState(StateMachine _stateMachine, BuildingController controller) : base(_stateMachine, controller)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        controller.isOnFire = true;
        controller.currentFireDamage = controller.maxFireDamage;
        controller.gameObject.tag = "BurningHouse";
    }

    public override void ExitState()
    {
        base.ExitState();
        controller.gameObject.tag = "House";
    }

    public override void Update()
    {
        base.Update();
        if (controller.currentFireDamage <= 0) 
        { 
            stateMachine.ChangeState(controller.idleState);
        }
        controller.currentHealthPoints -= controller.currentFireDamage * Time.deltaTime;
        if(controller.currentHealthPoints < 0)
        {
            Debug.Log("Building burnt");
            controller.gameObject.SetActive(false);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}