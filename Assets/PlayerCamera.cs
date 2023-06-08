using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //X Axis Sensitivity 
    public float sensX;
    //Y Axis Sensitivity 
    public float sensY;

    //Orientation of the Camera
    public Transform orientation;

    //Rotation by X 
    float xRotation;
    //Rotation by Y
    float yRotation;

    private void Start()
    {
        //Makes it so Cursor cannot move indepentantly of movement.
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor cannot be seen.  
        Cursor.visible = false;
    }

    private void Update()
    {
        //Get mouse input of X Axis.
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;

        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        //This Locks our X Rotation to 90 degrees. 
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Rotate Camera and Player Orientation.
        //Eulars Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis; applied in that order.
        //Eular Angles are important because they are used to describe the orientation of the reference frame relative to another reference frame.
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0,yRotation, 0);

    }
}
