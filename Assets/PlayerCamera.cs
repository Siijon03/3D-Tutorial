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

    }
}
