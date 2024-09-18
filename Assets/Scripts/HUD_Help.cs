using UnityEngine;
using TMPro;

public class HUD_Help : MonoBehaviour
{
    [Header("HUD Help")]
    public TextMeshProUGUI speedText; // Text component for displaying speed
    public TextMeshProUGUI currentSprintStatus; // Text component for displaying sprint status
    public TextMeshProUGUI wallJumpCondition; // Text component for displaying wall jump condition
    public TextMeshProUGUI currentPowerup; // Text component for displaying power-up

    [Header("Referenced Scripts")]
    PowerUpManager usedPowerup;

    // Update speed text
    public void UpdateSpeedText(float speed)
    {
        // Format speed to 2 decimal places.
        speedText.text = "Current Speed: " + speed.ToString("F1");
    }

    // Update sprint status.
    public void CurrentSprintStatusText(bool isSprint)
    {
        // Display "Sprinting" or "Normal" based on the boolean value.
        currentSprintStatus.text = "Sprint: " + (isSprint ? "Sprinting" : "Normal");
    }

    // Update wall jump status.
    public void UpdateWallJumpCondition(bool hasWallJumped)
    {
        // Display "Used" or "Ready" based on the boolean value
        wallJumpCondition.text = "Wall Jump: " + (hasWallJumped ? "Used" : "Ready");
    }

    // Update power-up text.
    public void CurrentPowerupText(string powerUpStatus)
    {
        // Display current power-up
        currentPowerup.text = "Current Power Up: " + powerUpStatus;
    }
}
