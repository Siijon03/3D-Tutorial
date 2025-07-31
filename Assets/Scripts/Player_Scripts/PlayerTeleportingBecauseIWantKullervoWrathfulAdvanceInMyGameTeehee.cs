using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleportingBecauseIWantKullervoWrathfulAdvanceInMyGameTeehee : MonoBehaviour
{

    [Header("Raycast Settings")]
    public float raycastRange = 100f;

    [Header("Script References")]
    public Transform player;         
    public GameObject playerRef;

    [Header("Offset Variable")]
    private Vector3 offsetRange;

    Vector3 targetPos;


    void Start()
    {
        // Start Function
        RandomizeOffset();
    }

    void Update()
    {
        // Player Input
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TryTeleport();
        }
    }

    void RandomizeOffset()
    {
        // Create a Random Interger for the 'Z' Axis 
        int offsetX = Random.Range(-2, -5);

        offsetRange = new Vector3(offsetX, 0f, 0f);
    }

    void TryTeleport()
    {
        // Create a Ray Cast that Moves Forward
        Ray ray = new Ray(transform.position, transform.forward);
        // Set up a Raycast 'Hit' Variable
        RaycastHit hit;

        // Raycast Parameters, Use the 'Range' to Control Teleporting Distance
        if (Physics.Raycast(ray, out hit, raycastRange))
        {
            // If the Tag of the Object is a 'TeleportTo' Object
            if (hit.collider.CompareTag("TeleportTo"))
            {
                // Use the Offset to Avoid Glitchy Interactions
                Vector3 randomOffset = new Vector3(offsetRange.x, offsetRange.y, offsetRange.z);
                Debug.Log(randomOffset);

                // Create a Target Position for a Player to Move to.
                targetPos = hit.collider.transform.position + randomOffset;
                TeleportPlayer(targetPos);
            }
        }
    }

    void TeleportPlayer(Vector3 targetPos)
    {
        // Briefly Disable Player.
        playerRef.SetActive(false);
        // Move the Player to that Target Postion.
        playerRef.transform.position = targetPos;
        playerRef.SetActive(true);
    }
}
