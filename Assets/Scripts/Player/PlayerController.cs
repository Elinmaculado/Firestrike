using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D characterController;
    float horizontalMovement;
    float verticalMovement;
    Vector3 movement;
    public float movementSpeed;
    public float dashForce;
    public float dashDuration;
    public float dashCooldown;
    bool isDashig = false;
    bool isReadyToDash = true;
    private void Update()
    {

        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");


        movement = new Vector3(horizontalMovement, verticalMovement, 0);

        if (Input.GetKeyDown(KeyCode.Space) && isReadyToDash)
        {
            characterController.AddForce(movement.normalized * dashForce, ForceMode2D.Impulse);
            StartCoroutine(DashDuration());
        }
    }

    private void FixedUpdate()
    {
        if (!isDashig)
        {
            characterController.velocity = movement.normalized * movementSpeed;
        }
    }

    IEnumerator DashDuration()
    {
        isDashig = true;
        isReadyToDash = false;
        yield return new WaitForSeconds(dashDuration);
        isDashig = false;
        characterController.velocity = Vector3.zero;
        yield return new WaitForSeconds(dashCooldown);
        isReadyToDash = true;
    }
}
