using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Basic_Movement : MonoBehaviour
{
    [Header("Movement")]
    //Freely able to change movement speed in the inspector.
    public float MovementSpeed;

    //Decreases acceleration and momentuem, this allows us to move more naturally. 
    public float groundDrag;

    [Header("Ground Check")]
    //Uses the Player's Height
    public float PlayerHeight;
    //Uses Raycast to Check whether we are on the ground
    public LayerMask GroundCheck;
    //True or False condition when we are either on/off the ground. 
    bool IsGrounded;

    [Header("Speed Checker")]
    public TMP_Text SpeedVarText;

    //Transforming the Orientation of an object.
    public Transform orientation;

    float HorizontalInput;
    float VerticalInput;

    Vector3 MovementDirection;

    //using the Ridgidbody of an Object.
    Rigidbody RB;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        //To prevent the player from falling over. 
        RB.freezeRotation = true;

    }

    //Will Constantly check for movement.
    private void Update()
    {
        SpeedVarText.SetText("Speed: " + MovementSpeed);

        PlayerInput();
        SpeedControl();

        //Consistently Checking for the Ground.
        IsGrounded = Physics.Raycast(transform.position + new Vector3(0,0.05f,0),Vector3.down, PlayerHeight * 0.5f + 0.3f, GroundCheck);

        //Ground and Air Drag.
        if (IsGrounded == true)
            RB.drag = groundDrag;
        else 
            RB.drag = 0;
    }

    //Used to check physics
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void PlayerInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");

    }

    private void MovePlayer()
    {
        //Move wherever you look.
        //The use of .Forward and .Right is to give direction to our orientation, this is also then multiplied by our inputs for movement. 
        MovementDirection = orientation.forward * VerticalInput + orientation.right * HorizontalInput;

        //Adds force to movement.
        RB.AddForce(10f * MovementSpeed * MovementDirection.normalized, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(RB.velocity.x, 0f, RB.velocity.z);

        //limit velocity if needed.
        if (flatVel.magnitude > MovementSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * MovementSpeed;
            //Controls and Limits Velocity on the X,Y,Z Axis
            RB.velocity = new Vector3(limitedVel.x, RB.velocity.y, limitedVel.z);
        }
    }
}
