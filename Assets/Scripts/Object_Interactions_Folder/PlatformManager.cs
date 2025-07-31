using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    // List of disappearing platforms
    [SerializeField]
    private List<GameObject> disappearingPlatforms = new List<GameObject>();

    // Individual disappear and reappear delays for each platform
    [SerializeField]
    private float disappearDelay = 2f;

    [SerializeField]
    private float reappearDelay = 5f;

    // Tracks the state of platforms' coroutines to avoid starting multiple coroutines for the same platform
    private Dictionary<GameObject, bool> platformStates = new Dictionary<GameObject, bool>();

    private void Start()
    {
        // Initialize the state tracking dictionary
        foreach (var platform in disappearingPlatforms)
        {
            // Initially, no platform is running a coroutine
            platformStates[platform] = false; 
        }
    }

    // Called when the player enters the trigger zone of any platform
    public void TriggerPlatformDisappearance(GameObject platform)
    {
        if (!platformStates[platform])  // Only start the coroutine if it's not already running
        {
            StartCoroutine(PlatformDisappearAndReappear(platform));
        }
    }

    private IEnumerator PlatformDisappearAndReappear(GameObject platform)
    {
        platformStates[platform] = true;  // Mark coroutine as running for this platform

        // Wait before making the platform disappear
        yield return new WaitForSeconds(disappearDelay);
        Debug.Log(platform.name + " is disappearing");
        platform.SetActive(false);

        // Wait before making the platform reappear
        yield return new WaitForSeconds(reappearDelay);
        Debug.Log(platform.name + " is reappearing");
        platform.SetActive(true);

        // Reset the state so that the platform can be triggered again in the future
        platformStates[platform] = false;
    }
}
