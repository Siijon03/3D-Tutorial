using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Basic_Movement : MonoBehaviour
{
    public CharacterController characterController;  // Reference to the CharacterController component

    // Values needed for movement
    [Header("Movement")]
    public float speed = 12.0f; // Normal movement speed
    public float sprintSpeed = 20.0f; // Sprint movement speed
    public float gravity = 0f; // Gravity applied to the player
    public float jumpHeight = 5f; // Height of the player's jump
    private bool isSprinting = false; // Tracks if the player is currently sprinting

    // Used to check if the player is touching the ground
    [Header("Ground Check")]
    public Transform groundCheck; // Position used to check if the player is on the ground
    public float groundDistance = 0.4f; // Radius for ground check
    public LayerMask groundMask; // Layer mask for ground detection

    // Used for player to look around
    public Transform cameraTransform; // Camera transform to align player movement with camera

    // Basic Vector3 for movement and boolean to check if the player is touching the ground
    private Vector3 velocity; // Player's current velocity
    private bool isGrounded; // Whether the player is grounded

    // Used to improve jumping movement
    private float coyoteTime = 0.2f; // Time after leaving the ground during which a jump is still possible
    private float coyoteTimeCounter; // Counter for coyote time

    // Adds a better buffer for jumping
    private float jumpBufferTime = 0.2f; // Time window to buffer jump input
    private float jumpBufferCounter; // Counter for jump buffer

    // Reference to HUD Help script to update HUD information
    public HUD_Help hudTextUpdate;

    // Track if the player has wall-jumped
    private bool hasWallJumped = false;

    private void Start()
    {
        // Get the CharacterController component
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if the player is grounded using a sphere cast
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Reset coyote time if grounded
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            // Decrease coyote time counter if in the air
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Check if the jump button was pressed
        if (Input.GetButtonDown("Jump"))
        {
            // Start or reset the jump buffer timer
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            // Decrease jump buffer counter over time
            jumpBufferCounter -= Time.deltaTime;
        }

        // Get player input for horizontal and vertical movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calculate movement direction based on player input and camera orientation
        Vector3 movement = cameraTransform.right * x + cameraTransform.forward * z;
        // Ensure no vertical movement from input
        movement.y = 0f;

        // Check if the sprint key is held down
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Move the character with sprint speed
            characterController.Move(movement * sprintSpeed * Time.deltaTime);
            isSprinting = true; // Set sprinting flag to true
        }
        else
        {
            // Move the character with normal speed
            characterController.Move(movement * speed * Time.deltaTime);
            isSprinting = false; // Set sprinting flag to false
        }

        // Apply gravity to the vertical component of the velocity
        velocity.y += gravity * Time.deltaTime;

        // Additional downward force when falling
        if (velocity.y < 0)
        {
            // Increase downward acceleration for a more realistic fall
            velocity.y += gravity * 1.5f * Time.deltaTime;
        }

        // Move the character with the updated velocity
        characterController.Move(velocity * Time.deltaTime);

        // Handle jumping if the jump buffer and coyote time conditions are met
        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            // Calculate vertical velocity for the jump
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            // Reset jump buffer counter
            jumpBufferCounter = 0;
        }

        // Reset vertical velocity if colliding with something above
        if ((characterController.collisionFlags & CollisionFlags.Above) != 0)
        {
            // Small downward force to push the player out of the ceiling
            velocity.y = -2f;
        }

        // Update HUD with current speed, sprint status, and wall jump condition
        hudTextUpdate.UpdateSpeedText(speed);
        hudTextUpdate.UpdateWallJumpCondition(hasWallJumped);
        hudTextUpdate.CurrentSprintStatusText(isSprinting);

        Debug.Log(isGrounded); // Debug log to check if player is grounded
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Check if the player is hitting a wall and not grounded
        if (!characterController.isGrounded && hit.normal.y < 0.1f)
        {
            Debug.DrawRay(hit.point, hit.normal, Color.yellow, 1.25f);
            Debug.Log("Hit Wall");

            // Stop horizontal movement upon hitting a wall
            velocity.x = 0;
            velocity.z = 0;

            // If the player presses the jump key while touching the wall, perform a wall jump
            if (Input.GetKey(KeyCode.Space))
            {
                // Calculate the wall jump direction
                Vector3 wallJumpDirection = hit.normal + Vector3.up * jumpHeight;
                wallJumpDirection.Normalize();

                // Apply new velocity for wall jump
                velocity = wallJumpDirection * speed * 1.5f;

                // Set wall jump condition to true
                hasWallJumped = true;
            }
            else
            {
                // Reset wall jump condition if player is not jumping
                hasWallJumped = false;
            }
        }
        else
        {
            // Reset wall jump condition if player is not touching a wall
            hasWallJumped = false;
        }
    }
}
