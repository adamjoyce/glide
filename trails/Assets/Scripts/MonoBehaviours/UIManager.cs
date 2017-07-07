using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject background;           // The tinted background that is shown when a menu is open.
    public GameObject pauseMenu;            // The pause menu that is displayed when Escape is pressed.
    public GameObject optionsMenu;          // The options menu displayed when 'Options' is clicked.

    private bool isPaused = false;          // Whether or not the game is currently paused.
    private GameObject openMenu;            // The menu that is currently open.

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
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    /* Resumes the game. */
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        HideMenu(openMenu);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        openMenu = null;
    }

    /* Displays the options menu. */
    public void ShowOptions()
    {
        HideMenu(pauseMenu);
        ShowMenu(optionsMenu);
        openMenu = optionsMenu;
    }

    /* Returns to the pause menu from the options menu. */
    public void BackToPauseMenu()
    {
        HideMenu(optionsMenu);
        ShowMenu(pauseMenu);
        openMenu = pauseMenu;
    }

    /* Pauses the game. */
    private void PauseGame()
    {
        Time.timeScale = 0.0f;
        ShowMenu(pauseMenu);
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        openMenu = pauseMenu;
    }

    /* Displays a menu. */
    private void ShowMenu(GameObject menu)
    {
        background.SetActive(true);
        menu.SetActive(true);
    }

    /* Hides a menu. */
    private void HideMenu(GameObject menu)
    {
        background.SetActive(false);
        menu.SetActive(false);
    }
}