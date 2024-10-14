using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class safetyNet : MonoBehaviour
{
    // Default position where the player will be teleported to
    [SerializeField]
    [Header("Customise Teleport Position")]
    Vector3 defaultPos = new Vector3(1, 5, 0);
    [Header("Script References")]
    public Transform player;
    public Transform tpDestination;
    public GameObject playerRef;

    // Stop Player From Falling off Map
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("Teleporting Player to " + tpDestination.position);
            playerRef.SetActive(false);
            playerRef.transform.position = defaultPos;
            playerRef.SetActive(true);
        }
    }
}
