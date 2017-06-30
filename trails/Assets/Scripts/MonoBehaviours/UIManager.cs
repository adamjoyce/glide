using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject background;           // The tinted background that is shown when a menu is open.
    public GameObject pauseMenu;            // The pause menu that is displayed when Escape is pressed.

    private bool isPaused = false;          // Whether or not the game is currently paused.

    /* Use this for initialization. */
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    /* Update is called once per frame. */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (!isPaused)
            {
                ShowPauseMenu();

            }
            else
            {
                HidePauseMenu();
            }
        }
        Debug.Log(Cursor.lockState);
    }

    /* Pauses the game and displays the pause menu. */
    private void ShowPauseMenu()
    {
        Time.timeScale = 0.0f;
        background.SetActive(true);
        pauseMenu.SetActive(true);
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    /* Resumes the game and hides the pause menu. */
    private void HidePauseMenu()
    {
        Time.timeScale = 1.0f;
        background.SetActive(false);
        pauseMenu.SetActive(false);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}