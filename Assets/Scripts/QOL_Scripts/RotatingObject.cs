using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    // Reference Game Objects
    [Header("Script Reference")]
    public GameObject rotObject;

    // Adjust Rotation Speed of Different Objects and Their Axis
    [Header("Rotation Speed Variables")]
    [SerializeField]
    private float rotatationSpeedX = 45f;
    [SerializeField]
    private float rotationSpeedY = 45f;
    [SerializeField]
    private float rotationSpeedZ = 45f;

    // Update is called once per frame
    void Update()
    {
        // Rotate Object in Real Time.
        rotObject.transform.Rotate(rotatationSpeedX * Time.deltaTime, rotationSpeedY * Time.deltaTime, rotationSpeedZ * Time.deltaTime);
    }
}
