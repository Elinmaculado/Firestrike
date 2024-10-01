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
    public SpriteRenderer sprite;

    public Animator animator;
    [SerializeField]GameObject tpParticles;
    [SerializeField] SpriteRenderer playerSprite;
    Color tpColor;
    private void Update()
    {

        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
        animator.SetInteger("Horizontal", (int)horizontalMovement);
        animator.SetInteger("Vertical", (int)verticalMovement);
        sprite.flipX = horizontalMovement < 0;

        movement = new Vector3(horizontalMovement, verticalMovement, 0);

        if (Input.GetKeyDown(KeyCode.Space) && isReadyToDash)
        {
            Instantiate(tpParticles,transform.position,tpParticles.transform.rotation);
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
        tpColor = playerSprite.color;
        tpColor.a = 0f;
        playerSprite.color = tpColor;
        yield return new WaitForSeconds(dashDuration);
        tpColor.a = 1f;
        playerSprite.color = tpColor;
        isDashig = false;
        characterController.velocity = Vector3.zero;
        yield return new WaitForSeconds(dashCooldown);
        isReadyToDash = true;
    }
}
