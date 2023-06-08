using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Movement : MonoBehaviour
{
    [Header("Movement")]
    //Freely able to change movement speed in the inspector.
    public float MovementSpeed;

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
        PlayerInput();
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
}
