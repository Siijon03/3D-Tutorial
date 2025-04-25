using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayerNoRotate : MonoBehaviour
{
    // Optionally, store the initial world position of the text
    public Vector3 initialPosition;

    void Start()
    {
        // Store the initial world position when the game starts
        initialPosition = transform.position;
    }

    void LateUpdate()
    {
        // Keep the position fixed in the world (it doesn't follow any parent)
        transform.position = initialPosition;
    }
}
