using UnityEngine;
using UnityEngine.UI;

public class TargetDotManager : MonoBehaviour
{
    public Image dotImage;
    public float raycastRange = 100f;

    [Header("Used Colours")]
    public Color normalColor = Color.white;
    public Color targetDetectedColor = Color.red;
    public Color interactableDetectedColor = Color.green;
    public Color powerupDetectedColor = Color.blueViolet;


    [Header("Used Layers")]
    public LayerMask teleportLayer;
    public LayerMask interactableLayer;
    public LayerMask powerupLayer;

    [Header("Pulse Settings")]
    public float pulseSpeed = 2f;
    public float pulseScale = 1.2f;

    private Camera mainCam;
    private Vector3 originalScale;
    private bool isTargeting;

    void Start()
    {
        mainCam = Camera.main;
        if (dotImage != null)
        {
            dotImage.color = normalColor;
            originalScale = dotImage.rectTransform.localScale;
        }
    }

    void Update()
    {
        Ray ray = new Ray(mainCam.transform.position, mainCam.transform.forward);
        RaycastHit hit;

        // Raycast Parameters, Use the 'Range' to Control Teleporting Distance
        if (Physics.Raycast(ray, out hit, raycastRange))
        {
            // If the Tag of the Object is a 'TeleportTo' Object
            if (hit.collider.CompareTag("TeleportTo"))
            {
                TargettingTeleport();
            }

            // If the Tag of the Object is a 'TeleportTo' Object
            if ((hit.collider.CompareTag("AntiGravityPickup")) || (hit.collider.CompareTag("JumpPickup")) || hit.collider.CompareTag("SpeedPickup"))
            {
                TargettingPickup();
            }

            else if (hit.collider.CompareTag("Interactable"))
            {
                TargettingInteractable();
            }
        }

        else
        {
            dotImage.color = normalColor;
            dotImage.rectTransform.localScale = originalScale;
        }
    }

    void TargettingTeleport()
    {
        Ray ray = new Ray(mainCam.transform.position, mainCam.transform.forward);
        RaycastHit hit;

        isTargeting = Physics.Raycast(ray, out hit, raycastRange, teleportLayer);

        // Change color
        dotImage.color = isTargeting ? targetDetectedColor : normalColor;

        // Apply pulse if targeting
        if (isTargeting)
        {
            float scale = Mathf.Lerp(1f, pulseScale, (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f);
            dotImage.rectTransform.localScale = originalScale * scale;
        }
        else
        {
            dotImage.rectTransform.localScale = originalScale;
        }
    }

    void TargettingInteractable()
    {
        Ray ray = new Ray(mainCam.transform.position, mainCam.transform.forward);
        RaycastHit hit;

        isTargeting = Physics.Raycast(ray, out hit, raycastRange, interactableLayer);

        // Change color
        dotImage.color = isTargeting ? interactableDetectedColor : normalColor;

        // Apply pulse if targeting
        if (isTargeting)
        {
            float scale = Mathf.Lerp(1f, pulseScale, (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f);
            dotImage.rectTransform.localScale = originalScale * scale;
        }
        else
        {
            dotImage.rectTransform.localScale = originalScale;
        }
    }

    void TargettingPickup()
    {
        Ray ray = new Ray(mainCam.transform.position, mainCam.transform.forward);
        RaycastHit hit;

        isTargeting = Physics.Raycast(ray, out hit, raycastRange, powerupLayer);

        // Change color
        dotImage.color = isTargeting ? powerupDetectedColor : normalColor;

        // Apply pulse if targeting
        if (isTargeting)
        {
            float scale = Mathf.Lerp(1f, pulseScale, (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f);
            dotImage.rectTransform.localScale = originalScale * scale;
        }
        else
        {
            dotImage.rectTransform.localScale = originalScale;
        }
    }
}
