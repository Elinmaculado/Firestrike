using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{

    public bool isOnFire;
    public float fireTime;
    public float fireTimer;
    public float maxFireDamage;
    public float currentFireDamage;
    [SerializeField] float maxHealthPoints;
    public float currentHealthPoints;
    StateMachine stateMachine;
    public BuildingIdleState idleState;
    public BuildingBurningState burningState;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new StateMachine();
        idleState = new BuildingIdleState(stateMachine,this);
        burningState = new BuildingBurningState(stateMachine, this);

        stateMachine.Initialize(idleState);

        currentHealthPoints = maxHealthPoints;
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.CurrentState.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    // Function must be called by projectiles or enemies that set the building on fire
    public void OnFire(float fireDamage)
    {
        fireTimer += fireDamage * Time.deltaTime;
    }


    // Function must be called by projectiles that put out fire
    public void PutOutFire(float waterDamage) 
    {
        currentFireDamage -= waterDamage;
    }
}
