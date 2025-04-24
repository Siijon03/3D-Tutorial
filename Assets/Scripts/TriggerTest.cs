using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Interacted!");
        Debug.Log(Random.Range(0, 100));
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            
        }
    }
}


