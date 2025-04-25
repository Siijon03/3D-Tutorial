using System.Collections;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    [SerializeField]
    private bool speedPickupActive = false;
    [SerializeField]
    private bool jumpPickupActive = false;
    [SerializeField]
    private bool antiGravityPickupActive = false;

    [SerializeField]
    [Header("Power Up Timer")]
    public float powerUpTimer = 5f;

    [Header("Power Up Multiplier")]
    public float speedMult = 2f;
    public float jumpMult = 2f;

    [SerializeField]
    [Header("Script References")]
    // Reference to Basic Movement in Order to Modify our Basic Attributes.
    public Basic_Movement moveRef;
    // Reference to HUD Help script to update HUD information.
    public HUD_Help hudTextUpdate;

    string currentPowerUp = "None";

    //Pass the Current PowerUp Status At the Start of the Game.
    void Start()
    {
        hudTextUpdate.CurrentPowerupText(currentPowerUp);
    }

    // OnTriggerEnter is called when another collider enters the trigger collider attached to the GameObject
    void OnTriggerEnter(Collider col)
    {
        // Debug.Log("Collided with: " + col.gameObject.name + " (Tag: " + col.gameObject.tag + ")");
        // Check if the collided object has the "SpeedPickup" tag
        if (col.CompareTag("SpeedPickup") && speedPickupActive == false)
        {
            // Start the Timer coroutine for the SpeedPickup power-up
            StartCoroutine(SpeedPowerUpRoutine(powerUpTimer, col.gameObject));
            //Update the Powerup Status on HUD Script.
            currentPowerUp = "Speed Powerup";
            hudTextUpdate.CurrentPowerupText(currentPowerUp);

            if (TryGetComponent<Basic_Movement>(out moveRef))
            {
                //Set the Power Up Flag to True, This is to Prevent Multiple Pickups of the Same Item.
                speedPickupActive = true;
                //Get the Speed From Basic Movement.
                moveRef.SetCurrentSpeed(moveRef.GetCurrentSpeed() * speedMult);
                moveRef.SetMaxSprintSpeed(moveRef.GetMaxSprintSpeed() * speedMult);
            }
            else
            {
                Debug.LogError("Basic Movement Script Has Not Been Found On Player, Please Assign It to Player.");
            }
        }

        // Check if the collided object has the "JumpPickup" tag
        else if (col.CompareTag("JumpPickup") && jumpPickupActive == false)
        {
            // Implement logic for JumpPickup
            StartCoroutine(JumpPowerUpRoutine(powerUpTimer, col.gameObject));
            //Update the Powerup Status on HUD Script.
            currentPowerUp = "Jump Powerup";
            hudTextUpdate.CurrentPowerupText(currentPowerUp);

            if (TryGetComponent<Basic_Movement>(out moveRef))
            {
                //Set the Power Up Flag to True, This is to Prevent Multiple Pickups of the Same Item.
                jumpPickupActive = true;
                moveRef.SetCurrentJumpHeight(moveRef.GetCurrentJumpHeight() * jumpMult);
            }
            else
            {
                Debug.LogError("Basic Movement Script Has Not Been Found On Player, Please Assign It to Player.");
            }
        }

        // Check if the collided object has the "JumpPickup" tag
        else if (col.CompareTag("AntiGravityPickup") && antiGravityPickupActive == false)
        {
            // Implement logic for JumpPickup
            StartCoroutine(AntiGravityRoutine(powerUpTimer, col.gameObject));
            //Update the Powerup Status on HUD Script.
            currentPowerUp = "Anti-Gravity Powerup";
            hudTextUpdate.CurrentPowerupText(currentPowerUp);

            if (TryGetComponent<Basic_Movement>(out moveRef))
            {
                //Set the Power Up Flag to True, This is to Prevent Multiple Pickups of the Same Item.
                antiGravityPickupActive = true;
                moveRef.SetPlayerGravity(moveRef.GetPlayerGravity() + 7.8f);
            }
            else
            {
                Debug.LogError("Basic Movement Script Has Not Been Found On Player, Please Assign It to Player.");
            }
        }

        else
        {
            Debug.Log("Powerup is Already Active");
        }
    }

    // Coroutine to Handle the Powerup Timer.
    IEnumerator SpeedPowerUpRoutine(float powerUpDuration, GameObject go)
    {
        go.SetActive(false);

        Debug.Log("Move Faster!");
        yield return new WaitForSeconds(powerUpDuration);

        if (speedPickupActive == true)
        {
            //Set the Condition Back to False so the Player can pick up the Object Again Without Stacking.
            //Also Inverse the PowerUp Process.
            speedPickupActive = false;
            moveRef.SetCurrentSpeed(moveRef.GetCurrentSpeed() / speedMult);
            moveRef.SetMaxSprintSpeed(moveRef.GetMaxSprintSpeed() / speedMult);
        }

        currentPowerUp = "None";
        hudTextUpdate.CurrentPowerupText(currentPowerUp);
        go.SetActive(true);
    }

    // Coroutine to Handle the Jump Timer.
    IEnumerator JumpPowerUpRoutine(float powerUpDuration, GameObject go)
    {
        go.SetActive(false);

        Debug.Log("Jump Higher!");
        yield return new WaitForSeconds(powerUpDuration);

        if (jumpPickupActive == true)
        {
            jumpPickupActive = false;
            moveRef.SetCurrentJumpHeight(moveRef.GetCurrentJumpHeight() / jumpMult);
        }

        currentPowerUp = "None";
        hudTextUpdate.CurrentPowerupText(currentPowerUp);
        go.SetActive(true);
    }

    // Coroutine to Handle Anti Gravity.
    IEnumerator AntiGravityRoutine(float powerUpDuration, GameObject go)
    {
        go.SetActive(false);

        Debug.Log("Floaty...!");
        yield return new WaitForSeconds(powerUpDuration);

        if (antiGravityPickupActive == true)
        {
            antiGravityPickupActive = false;
            moveRef.SetPlayerGravity(moveRef.GetPlayerGravity() - 7.8f);
        }

        currentPowerUp = "None";
        hudTextUpdate.CurrentPowerupText(currentPowerUp);
        go.SetActive(true);
    }
}
