using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{

    [SerializeField] private GameObject promptText;

    private void Start()
    {
        if (promptText != null)
            promptText.SetActive(false); // Keep it hidden at the start

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && promptText != null)
        {
            promptText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && promptText != null)
        {
            promptText.SetActive(false);
        }
    }

    private void Update()
    {
        // Make prompt face the camera
        if (promptText != null && promptText.activeSelf && Camera.main != null)
        {
            promptText.transform.LookAt(Camera.main.transform);
            promptText.transform.Rotate(0, 180f, 0); // Flip to face the player
        }
    }

}
