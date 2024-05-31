using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Basic_Movement : MonoBehaviour
{
    public CharacterController characterController;  // Reference to the CharacterController component

    [Header("Movement")]
    // Normal movement speed
    public float speed = 12.0f;  
    // Sprint movement speed
    public float sprintSpeed = 20.0f;  
    // Gravity value, increased to make falling faster
    public float gravity = -20f;  
    // Jump height, decreased to reduce airtime
    public float jumpHeight = 5f;  

    [Header("Ground Check")]
    // Transform for ground check position
    public Transform groundCheck;
    // Radius for ground check sphere
    public float groundDistance = 0.4f;  
    // Layer mask to specify what counts as ground
    public LayerMask groundMask;

    // Transform of the camera to determine facing direction
    public Transform cameraTransform;

    // Player's velocity
    private Vector3 velocity;  
    // Whether the player is grounded
    private bool isGrounded;

    // Time after leaving ground where a jump is still possible
    private float coyoteTime = 0.2f;
    // Counter for coyote time
    private float coyoteTimeCounter;

    // Time window to buffer jump input
    private float jumpBufferTime = 0.2f;
    // Counter for jump buffer
    private float jumpBufferCounter; 

    void Update()
    {
        // Check if the player is grounded by casting a sphere at the groundCheck position
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Reset coyote time counter if grounded
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Handle jump buffer input
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // Get player input for horizontal and vertical movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calculate movement direction based on input and the direction the player is facing
        Vector3 movement = cameraTransform.right * x + cameraTransform.forward * z;

        // Remove any upward/downward component of the movement direction
        movement.y = 0f;

        // Move the character using the CharacterController
        if (Input.GetKey(KeyCode.LeftShift))  // Check if the sprint key is held down
        {
            // Move with sprint speed
            characterController.Move(movement * sprintSpeed * Time.deltaTime);  
        }
        else
        {
            // Move with normal speed
            characterController.Move(movement * speed * Time.deltaTime);  
        }

        // Apply gravity to the velocity
        velocity.y += gravity * Time.deltaTime;

        // Additional downward force when falling to make the jump less floaty
        if (velocity.y < 0)
        {
            // Apply extra downward force
            velocity.y += gravity * 1.5f * Time.deltaTime;  
        }

        // Move the character downwards due to gravity
        characterController.Move(velocity * Time.deltaTime);

        // Handle jumping with coyote time and jump buffer
        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            // Calculate the jump velocity
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            // Reset jump buffer counter
            jumpBufferCounter = 0;  
        }

        if ((characterController.collisionFlags & CollisionFlags.Above) != 0)
        {
            velocity.y = -2f;
        }
    }
}
