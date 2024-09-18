using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Menu : MonoBehaviour
{
    public static bool isPaused;
    public GameObject pauseUI;

    private void Start()
    {
        pauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();

            }
            else
            {
                PauseGame();
            }
        }
    }

    void ResumeGame()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;

    }

    void PauseGame()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        //Makes it so Cursor cannot move indepentantly of movement.
        Cursor.lockState = CursorLockMode.None;
        //Cursor cannot be seen.  
        Cursor.visible = true;
    }

    public void ResumeButton()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
        //Makes it so Cursor cannot move indepentantly of movement.
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor cannot be seen.  
        Cursor.visible = false;
    }

    public void SettingsButtons()
    {
        Debug.Log("Insert Settings Menu!!");
    }

    public void onQuitButton()
    {
        Application.Quit();
    }

}
