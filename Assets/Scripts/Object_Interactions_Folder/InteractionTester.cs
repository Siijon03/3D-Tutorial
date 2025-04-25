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
    private LayerMask interactableLayer; // Set this to "Interactable" in the Inspector

    [SerializeField]
    [Header("Script Variables")]
    public Transform InteractorSource;
    [SerializeField]
    public float InteractRange;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            Ray r = new Ray(camRef.transform.position, camRef.CameraForward);
            Debug.DrawLine(camRef.transform.position, camRef.transform.position + camRef.CameraForward * InteractRange, Color.red, 5f);

            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange, interactableLayer))
            {
                if (hitInfo.collider.CompareTag("Interactable"))
                {
                    if (hitInfo.collider.TryGetComponent(out IInteractable interactObj))
                    {
                        interactObj.Interact();
                    }

                }

            }
        }
    }

}



