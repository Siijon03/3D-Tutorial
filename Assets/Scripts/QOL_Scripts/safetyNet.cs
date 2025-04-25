using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class safetyNet : MonoBehaviour
{
    // Default position where the player will be teleported to
    [SerializeField]
    [Header("Customise Teleport Position")]
    Vector3 defaultPos = new Vector3(1, 5, 0);
    //Reference Objects needed for Script to Work
    [Header("Script References")]
    public Transform player;
    public Transform tpDestination;
    public GameObject playerRef;

    // Stop Player From Falling off Map
    void OnTriggerEnter(Collider col)
    {
        // Activate if Player is Falling off Map
        if (col.CompareTag("Player"))
        {
            //Briefly make the Player Disappear to make teleport work.
            playerRef.SetActive(false);
            //Teleport the Player to their Spawn Point.
            playerRef.transform.position = defaultPos;
            playerRef.SetActive(true);
        }
    }
}
