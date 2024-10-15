using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Reference to the player character
    [SerializeField]
    private GameObject player;

    // Boolean to track if the player is on the platform
    private bool isOnPlatform = false;

    // Store the player's initial offset when they step on the platform
    private Vector3 playerOffset;

    private void Update()
    {
        // Check for jump input to detach from the platform
        if (isOnPlatform && Input.GetKey(KeyCode.Space))
        {
            DetachFromPlatform();
        }
    }

    private void FixedUpdate()
    {
        // If the player is on the platform, move with it
        if (isOnPlatform && player != null)
        {
            // Update player's position based on the platform's position and their initial offset
            player.transform.position = transform.position + playerOffset;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        // Check if the object entering the trigger is the player
        if (col.CompareTag("Player"))
        {
            Debug.Log("Player has entered the platform.");
            player = col.gameObject; // Store the player's reference

            // Calculate and store the initial offset from the platform's position
            playerOffset = player.transform.position - transform.position;
            isOnPlatform = true; // Set flag to true
        }
    }

    private void OnTriggerExit(Collider col)
    {
        // Check if the object exiting the trigger is the player
        if (col.CompareTag("Player"))
        {
            Debug.Log("Player has exited the platform.");
            DetachFromPlatform();
        }
    }

    private void DetachFromPlatform()
    {
        Debug.Log("Player has detached from the platform.");
        player = null; // Clear the player's reference
        isOnPlatform = false; // Set flag to false
    }
}
