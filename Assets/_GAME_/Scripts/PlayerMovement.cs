using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.6f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isSprinting;
    private float CurrentSpeed => moveSpeed * (isSprinting ? sprintMultiplier : 1f);

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        // Get input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize(); // Prevent faster diagonal movement
        isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift); //sprinting

        // animation
        bool isMoving = movement.x != 0 || movement.y != 0;
        animator.SetBool("isWalking", isMoving);

        // flip sprite (only for horizontal movement)
        if (movement.x > 0)
            spriteRenderer.flipX = false;
        else if (movement.x < 0)
            spriteRenderer.flipX = true;
    }

    void FixedUpdate()
    {
      
        // Move the player
        rb.MovePosition(rb.position + movement * CurrentSpeed * Time.fixedDeltaTime);
    }
}
