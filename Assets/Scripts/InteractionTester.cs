using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Interact();
}

public class InteractionTester : MonoBehaviour
{

    [SerializeField]
    [Header("Script Reference")]
    public PlayerCamera camRef;

    [SerializeField]
    [Header("Script Variables")]
    public Transform InteractorSource;
    [SerializeField]
    public float InteractRange;

    // Update is called once per frame
    void Update()
    {
        //Interaction Method
        if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton1)) {
            // Create a Ray from the Source (In this Case, Player Camera's Position.) Via Referencing the Players Camera.
            Ray r = new Ray(camRef.transform.position, camRef.CameraForward);
            // Create a Ray Cast Line for Checking Interactions.
            Debug.DrawLine(camRef.transform.position, camRef.transform.position + camRef.CameraForward * 5f, Color.red, 5f);
            // If the Object is within our Interaction Range.
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                // Use the Collider from the GameObject to know.
                if (hitInfo.collider.TryGetComponent(out IInteractable interactObj))
                {
                    // Use the Interact Function From Another Script. 
                    interactObj.Interact();
                    Debug.Log("Interacted!");
                }
                else
                {
                    Debug.Log("Not Interacted!");
                }

            }

        }
    }
}
