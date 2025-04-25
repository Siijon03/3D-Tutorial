using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeCongText_Appear : MonoBehaviour
{

    [SerializeField] private GameObject congratsText;

    // Start is called before the first frame update
    void Start()
    {
        congratsText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && congratsText != null)
        {
            congratsText.SetActive(true);
        }
    }
}
