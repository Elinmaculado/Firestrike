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
        controller.burningSprite.transform.localScale = Vector3.one* controller.currentFireDamage;
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
        controller.burningSprite.transform.localScale = Vector3.one * controller.currentFireDamage;
        if (controller.currentHealthPoints < 0)
        {
            Debug.Log("Building burnt");
            GameManager.instance.BurnHouse();
            controller.gameObject.SetActive(false);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}