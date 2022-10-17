using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private Vector2 inputVector;
    [SerializeField]
    int jumpForce = 200,
    playerSpeed = 200,
    maxSpeed = 4;
    [SerializeField]
    private LayerMask groundLayer;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        inputVector = Vector2.zero;
        animator = GetComponent<Animator>();
    }

    
    void FixedUpdate()
    {   
        Move();
        handleAnimations();
        flipTowardsMovement();
        isGrounded();
    }

    // TODO: Add a 2D force vector to the rigidbody to make the player jump. Do not let the player jump while in the air.
    // Hint: rb.AddForce(forceVector, ForceMode2D.Impulse);
    public void Jump(InputAction.CallbackContext context) {
        if (context.performed && isGrounded()) {
            rb.AddForce(jumpForce * new Vector2(0, 1), ForceMode2D.Impulse);
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context) {
        inputVector = context.ReadValue<Vector2>();
        flipTowardsMovement();
    }

    // TODO: Add a 2D force vector to the rigidbody to move the player based on the inputVector. Do not let the player move faster than the max speed.
    // Hint: rb.AddForce(movementForce);
    private void Move() {
        if (Mathf.Abs(rb.velocity.x) < maxSpeed) {
            rb.AddForce(new Vector2(inputVector.x, 0) * playerSpeed);
        }
    }

    private bool isGrounded() {
        float heightControlParam = 0.2f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider.bounds.center,capsuleCollider.bounds.size, 0, Vector2.down, heightControlParam, groundLayer);
        return raycastHit.collider != null;
    }

    //TODO: Set the players transform.localScale to a new 3D vector based on the inputVector.
    private void flipTowardsMovement() {
        if (inputVector.x != 0) {
            rb.transform.localScale =
                new Vector3((inputVector.x >= 0) ? 1 : -1, 1, 1);
        }
    }

    // TODO: Set the "Falling" and "Running" parameters of the animator component. 
    // Hint: Rigidbodies have a velocity vector we can check.
    private void handleAnimations() {
        animator.SetBool("Falling", rb.velocity.y != 0);
        animator.SetBool("Running", Mathf.Abs(rb.velocity.x) > 2);
    }
}
