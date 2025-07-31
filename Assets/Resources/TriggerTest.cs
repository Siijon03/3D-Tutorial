using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using DialogueEditor;

public class TriggerTest : MonoBehaviour, IInteractable
{

    [Header("Hook up Game Object to Converse With")]
    public NPCConversation playerConversation;
    [Header("Script References")]
    [SerializeField]
    private GameObject promptText;
    public Basic_Movement moveRef;
    public GameObject hideTargetReticle;
    [Header("Script Variables")]
    bool isTalking;

    public void Interact()
    {
        ConversationManager.Instance.StartConversation(playerConversation);
        ConversationActive();
    }

    public void EndInteraction()
    {
        ConversationManager.Instance.EndConversation();
        ConversationDeActive();
    }

    private void Awake()
    {
        if (promptText == null)
        {
            promptText = GameObject.Find("Text (TMP)"); // Replace with your actual object name
            Debug.Log("Auto-assigned promptText: " + promptText);
        }
    }


    /*void Update()
    {
        if (isTalking && !ConversationManager.Instance.IsConversationActive)
        {
            EndInteraction();
        }
    }*/

    public void ConversationActive()
    {
        isTalking = true;
        if (isTalking == true)
        {
            promptText.SetActive(false);
            moveRef.SetCurrentSpeed(moveRef.GetCurrentSpeed() * 0);
            moveRef.SetMaxSprintSpeed(moveRef.GetMaxSprintSpeed() * 0);
            moveRef.SetCurrentJumpHeight(moveRef.GetCurrentJumpHeight() * 0);
            hideTargetReticle.SetActive(false);
            //Makes it so Cursor is Active to Select Options.
            Cursor.lockState = CursorLockMode.None;
            //Cursor cannot be seen.  
            Cursor.visible = true;
        }
    }

    public void ConversationDeActive()
    {
        if (promptText != null)
            promptText.SetActive(true);

        isTalking = false;
        if (isTalking == false)
        {
            promptText.SetActive(true);
            moveRef.SetCurrentSpeed(moveRef.GetCurrentSpeed() + 12);
            moveRef.SetMaxSprintSpeed(moveRef.GetMaxSprintSpeed() + 24);
            moveRef.SetCurrentJumpHeight(moveRef.GetCurrentJumpHeight() + 5f);
            hideTargetReticle.SetActive(true);
            //Makes it so Cursor cannot move indepentantly of movement.
            Cursor.lockState = CursorLockMode.Locked;
            //Cursor cannot be seen.  
            Cursor.visible = false;
            ConversationManager.Instance.EndConversation();
        }
    }
}
