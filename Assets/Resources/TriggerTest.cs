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
    [SerializeField]
    public Basic_Movement moveRef;
    public GameObject hideTargetReticle;
    [Header("Script Variables")]
    bool isTalking;
    bool EndConversation;

    // IMPORTANT NOTE: When Using the Dialogue Editor Tool, End the Conversation using a Boolean.
    // This will Pass Through to the Script and End the Conversation Properly. 
    // Also Overwrite your Prefeb each Time when Testing... or make a seperate one. 

    // When Script is Active, Check if the Prompt Check is Valid
    private void Awake()
    {
        if (promptText == null)
        {
            promptText = GameObject.Find("Text (TMP)"); // Replace with your actual object name
            Debug.Log("Auto-assigned promptText: " + promptText);
        }
    }

    // This Function is Started with 'InteractionTester'
    public void Interact()
    {
        // Start the Conversation
        ConversationManager.Instance.StartConversation(playerConversation);
        // Start the Function
        ConversationActive();
    }

    public void EndInteraction()
    {
        // End the Conversation
        ConversationManager.Instance.EndConversation();
        // Pass the Boolean Through to serve as a 'Check.'
        ConversationDeActive(EndConversation);
    }

    //Using a Update to Double Ensure the Conversation ends Properly.
    void Update()
    {
        if (isTalking && !ConversationManager.Instance.IsConversationActive)
        {
            EndInteraction();
        }
    }

    public void ConversationActive()
    {
        // Activates on the Condition of if the Player is Talking.
        isTalking = true;
        if (isTalking == true)
        {
            // Hide 'E to Interact Prompt.'
            promptText.SetActive(false);
            // Freeze Movement During Conversation to 'Lock the Player.'
            moveRef.SetCurrentSpeed(moveRef.GetCurrentSpeed() * 0);
            moveRef.SetMaxSprintSpeed(moveRef.GetMaxSprintSpeed() * 0);
            moveRef.SetCurrentJumpHeight(moveRef.GetCurrentJumpHeight() * 0);
            // Hide Reticle during Conversation. 
            hideTargetReticle.SetActive(false);
            //Makes it so Cursor is Active to Select Options.
            Cursor.lockState = CursorLockMode.None;
            //Cursor cannot be seen.  
            Cursor.visible = true;
        }
    }

    public void ConversationDeActive(bool EndConversation)
    {
        // If the 'PromptText' is still there, Set it to True.
        if (promptText != null)
            promptText.SetActive(true);

        // Deactivatates on the Condition of if the Player is Talking.
        isTalking = false;
        if (isTalking == false)
        {
            promptText.SetActive(true);
            // Restore Movement Values to their Original State to Allow Movement. 
            moveRef.SetCurrentSpeed(12);
            moveRef.SetMaxSprintSpeed(24);
            moveRef.SetCurrentJumpHeight(10);
            // Show Reticle after Conversation. 
            hideTargetReticle.SetActive(true);
            //Makes it so Cursor cannot move indepentantly of movement.
            Cursor.lockState = CursorLockMode.Locked;
            //Cursor cannot be seen.  
            Cursor.visible = false;
            // End the Conversation using the Dialogue Manager Script. 
            ConversationManager.Instance.EndConversation();
        }
    }
}
