using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    // Reference to the PlatformManager
    [SerializeField]
    private PlatformManager platformManager;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log(gameObject.name + " trigger activated.");
            // Pass the platform to the manager
            platformManager.TriggerPlatformDisappearance(gameObject);  
        }
    }
}
