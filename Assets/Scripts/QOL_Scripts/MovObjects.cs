using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovObjects : MonoBehaviour
{

    // Reference Game Object.
    [Header("Script Reference")]
    public GameObject moveObject;

    // Create Variables That Can be Accessed within the Editor. 
    [Header("Script Variables")]
    [SerializeField]
    private float movementSpeed = 1f;
    [SerializeField]
    private float upDirectionLimiter = 1f;

    private Vector3 originalPosition;
    private Vector3 targetUpPosition;

    void Start()
    {
        // Store the initial position of the object.
        originalPosition = transform.position;

        // Set the target position a set amount of units above the original position.
        targetUpPosition = originalPosition + new Vector3(0, upDirectionLimiter, 0);

        // Start the up and down movement
        StartCoroutine(MoveObject());
    }

    public IEnumerator MoveObject()
    {
        // Infinite loop to keep the object moving up and down
        while (true)
        {
            // Move up based on the upSpeed
            yield return StartCoroutine(SmoothMove(transform.position, targetUpPosition, movementSpeed));

            // Move down based on the upSpeed
            yield return StartCoroutine(SmoothMove(transform.position, originalPosition, movementSpeed));

            // Optional: add a short pause between each cycle
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator SmoothMove(Vector3 startPos, Vector3 endPos, float speed)
    {
        // Calculate the duration based on the speed (higher speed means shorter duration)
        float duration = 1f / speed; // Adjust the divisor to control the rate more finely
        float elapsedTime = 0f;

        // Smoothly move between the two positions
        while (elapsedTime < duration)
        {
            // Smoothly interpolates the time (t) value between 0 and 1 based on the elapsed time and total duration.
            // SmoothStep creates a smoother acceleration and deceleration curve, making the movement more natural.
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / duration);

            // Linearly interpolates between the starting position (startPos) and the target position (endPos) using the t value.
            // As t goes from 0 to 1, the object moves smoothly between the two points.
            transform.position = Vector3.Lerp(startPos, endPos, t);

            // Increment the elapsed time by the time passed since the last frame, keeping track of how much time has passed in total.
            elapsedTime += Time.deltaTime;

            // Wait until the next frame before continuing the loop to gradually complete the movement.
            yield return null;
        }

        // Ensure the object reaches the exact final position
        transform.position = endPos;
    }

}
