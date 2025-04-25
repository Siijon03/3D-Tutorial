using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Basic_Movement : MonoBehaviour
{
    public CharacterController characterController;  // Reference to the CharacterController component

    // Values needed for movement
    [Header("Movement Variables")]
    // Normal movement speed.
    public float speed = 12.0f;
    // Sprint movement speed.
    public float maxSprintSpeed = 20.0f;
    //Create A Variable that Will Control the Rate of The Player Speeding Up. (This is to Make Sprinting Smoother)
    public float sprintAcceleration = 2f;
    // Gravity applied to the player.
    public float gravity = -9.80f;
    // Height of the player's jump.
    public float jumpHeight = 5f; 
    // Tracks if the player is currently sprinting.
    private bool isSprinting = false; 
    //Tracks The Player's Current Speed.
    private float currentSpeed = 0f;

    // Used to check if the player is touching the ground.
    [Header("Ground Check")]
    // Position used to check if the player is on the ground.
    public Transform groundCheck;
    // Radius for ground check.
    public float groundDistance = 0.4f; 
    // Layer mask for ground detection.
    public LayerMask groundMask;

    // Used for player to look around.
    // Camera transform to align player movement with camera.
    [Header("Player Camera Reference")]
    public Transform cameraTransform; 

    // Basic Vector3 for movement and boolean to check if the player is touching the ground.
    // Player's current velocity.
    private Vector3 velocity;
    // Whether the player is grounded.
    private bool isGrounded; 

    // Used to improve jumping movement
    private float coyoteTime = 0.2f; // Time after leaving the ground during which a jump is still possible.
    private float coyoteTimeCounter; // Counter for coyote time.

    // Adds a better buffer for jumping
    private float jumpBufferTime = 0.2f; // Time window to buffer jump input.
    private float jumpBufferCounter; // Counter for jump buffer.

    [Header("Player HUD Reference")]
    // Reference to HUD Help script to update HUD information.
    public HUD_Help hudTextUpdate;

    // Track if the player has wall-jumped.
    private bool hasWallJumped = false;

    private void Start()
    { 
        // Get the CharacterController component.
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if the player is grounded using a sphere cast.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Reset coyote time if grounded.
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            // Decrease coyote time counter if in the air.
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Check if the jump button was pressed.
        if (Input.GetButtonDown("Jump"))
        {
            // Start or reset the jump buffer timer.
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            // Decrease jump buffer counter over time.
            jumpBufferCounter -= Time.deltaTime;
        }

        // Get player input for horizontal and vertical movement.
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calculate movement direction based on player input and camera orientation.
        Vector3 movement = cameraTransform.right * x + cameraTransform.forward * z;
        //Using a Vector to Get the Current Values of Our 'Speed' this is done by getting the Object's Transform on the X and Z Axis. 
        Vector3 movementSpeed = transform.right * x + transform.forward * z;
        // Ensure no vertical movement from input.
        movement.y = 0f;

        //Check if the sprint key is held down.
        //The Use of .magnitude is used as we need the length of the Vector being used for movement speed, this is also the to ensure the player can only sprint whilst moving around instead of sprinting on the spot.
        //Mathf. Lerp is used to move from one value to another over a specified period of time.
        if (Input.GetKey(KeyCode.LeftShift) && movementSpeed.magnitude > 0)
        {
            // Move the character with sprint speed
            characterController.Move(movement * currentSpeed * Time.deltaTime);
            // Set Sprinting Condition to true.
            isSprinting = true;
            currentSpeed = Mathf.Lerp(currentSpeed, maxSprintSpeed, sprintAcceleration * Time.deltaTime);
        }
        //Change the Speedometer to Zero when not Moving.
        else if (movementSpeed.magnitude == 0)
        {
            //Decreases to 0 if not moving.
            //Uses the 'sprintAcceleration' variable to adjust the rate of increase/decrease.
            currentSpeed = Mathf.Lerp(currentSpeed, 0, sprintAcceleration * Time.deltaTime);
        }
        //In Cast Regular Speed is Somehow Faster than Sprint Speed.
        else if (speed > maxSprintSpeed)
        {
            maxSprintSpeed = speed * 2;
        }
        else
        {
            // Move the character with sprint speed
            characterController.Move(movement * currentSpeed * Time.deltaTime);
            // Set Sprinting Condition to False.
            isSprinting = false;
            currentSpeed = Mathf.Lerp(currentSpeed, speed, sprintAcceleration * Time.deltaTime);
        }

        // Apply gravity to the vertical component of the velocity.
        velocity.y += gravity * Time.deltaTime;

        // Additional downward force when falling.
        if (velocity.y < 0)
        {
            // Increase downward acceleration for a more realistic fall.
            velocity.y += gravity * 1.5f * Time.deltaTime;
        }

        // Move the character with the updated velocity.
        characterController.Move(velocity * Time.deltaTime);

        // Handle jumping if the jump buffer and coyote time conditions are met.
        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            // Calculate vertical velocity for the jump.
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            // Reset jump buffer counter.
            jumpBufferCounter = 0;
        }

        // Reset vertical velocity if colliding with something above.
        if ((characterController.collisionFlags & CollisionFlags.Above) != 0)
        {
            // Small downward force to push the player out of the ceiling.
            velocity.y = -2f;
        }

        // Update HUD with current speed, sprint status, and wall jump condition.
        hudTextUpdate.UpdateSpeedText(currentSpeed);
        hudTextUpdate.UpdateWallJumpCondition(hasWallJumped);
        hudTextUpdate.CurrentSprintStatusText(isSprinting);

        // Debug log to check if player is grounded.
        // Debug.Log(isGrounded); 
    }

    //Use of Getter and Setters that Will Be Used for Reference in Another Script
    public float GetCurrentSpeed()
    {
        return speed;
    }

    public void SetCurrentSpeed(float setSpeed)
    {
        speed = setSpeed;
    }

    public float GetMaxSprintSpeed()
    {
        return maxSprintSpeed;
    }

    public void SetMaxSprintSpeed(float setNewSprint)
    {
        maxSprintSpeed = setNewSprint;
    }

    public float GetCurrentJumpHeight()
    {
        return jumpHeight;
    }

    public void SetCurrentJumpHeight(float setJumpHeight)
    {
        jumpHeight = setJumpHeight;
    }

    public float GetPlayerGravity()
    {
        return gravity;

    }

    public void SetPlayerGravity(float setNewgravity)
    {
        gravity = setNewgravity;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Check if the player is hitting a wall and not grounded
        if (!characterController.isGrounded && hit.normal.y < 0.1f)
        {
            Debug.DrawRay(hit.point, hit.normal, Color.yellow, 1.25f);
            Debug.Log("Hit Wall");

            // Stop horizontal movement upon hitting a wall.
            velocity.x = 0;
            velocity.z = 0;

            // If the player presses the jump key while touching the wall, perform a wall jump.
            if (Input.GetKey(KeyCode.Space))
            {
                // Calculate the wall jump direction.
                Vector3 wallJumpDirection = hit.normal + Vector3.up * jumpHeight;
                wallJumpDirection.Normalize();

                // Apply new velocity for wall jump
                velocity = wallJumpDirection * speed * 1.5f;

                // Set wall jump condition to true.
                hasWallJumped = true;

                // Stop horizontal movement upon jumping off a wall, this is to prevent unnecessary sliding.
                velocity.x = 0;
                velocity.z = 0;
            }
            else
            {
                // Reset wall jump condition if player is not jumping.
                hasWallJumped = false;
            }
        }


        else
        {
            // Reset wall jump condition if player is not touching a wall.
            hasWallJumped = false;
        }

    }
}
