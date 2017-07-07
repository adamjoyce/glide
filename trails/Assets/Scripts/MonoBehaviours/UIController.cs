using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public SceneController sceneController;     // The scene controller which handles scene fades and switching.
    public GameObject background;               // The tinted background that is shown when a menu is open.
    public GameObject mainMenu;                 // The main menu that is displayed before starting the game.
    public GameObject pauseMenu;                // The pause menu that is displayed when Escape is pressed.
    public GameObject optionsMenu;              // The options menu displayed when 'Options' is clicked.
    public GameObject startText;                // The text informing the player to press a button to continue.

    private bool isPaused = false;              // Whether or not the game is currently paused.
    private GameObject openMenu;                // The menu that is currently open.
    private GameObject previousMenu;            // The menu that was open before the current one.

    /* Use this for initialization. */
    void Start()
    {
        openMenu = mainMenu;
    }

    /* Update is called once per frame. */
    void Update()
    {
        if (startText.activeInHierarchy && Input.anyKeyDown)
        {
            startText.SetActive(false);
            ShowMenu(mainMenu);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
                openMenu = null;
            }
        }
    }

    /* Displays a menu. */
    public void ShowMenu(GameObject menu)
    {
        menu.SetActive(true);
        previousMenu = openMenu;
        openMenu = menu;
    }

    /* Hides a menu. */
    public void HideMenu(GameObject menu)
    {
        menu.SetActive(false);
    }

    /* Returns to the previous menu. */
    public void BackToMenu()
    {
        HideMenu(openMenu);
        ShowMenu(previousMenu);
    }

    /* Starts the game. */
    public void PlayGame()
    {
        sceneController.FadeAndLoadScene("MeteorStrike");
        HideMenu(mainMenu);
        Cursor.lockState = CursorLockMode.Locked;
    }

    /* Pauses the game. */
    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        ShowMenu(pauseMenu);
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    /* Resumes the game. */
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        HideMenu(openMenu);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /* Quits the game. */
    public void QuitGame()
    {
        Application.Quit();

        // For exiting the editor.
        UnityEditor.EditorApplication.isPlaying = false;
    }
}