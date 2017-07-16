using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public SceneController sceneController;     // The scene controller which handles scene fades and switching.
    public AudioController audioController;     // The audio controller which manages the game's audio levels.
    public Options options;                     // The options script which manages the options values and player prefs.
    public GameObject background;               // The tinted background that is shown when a menu is open.
    public GameObject mainMenu;                 // The main menu that is displayed before starting the game.
    public GameObject pauseMenu;                // The pause menu that is displayed when Escape is pressed.
    public GameObject optionsMenu;              // The options menu that is displayed when 'Options' is clicked.
    public GameObject gameOverMenu;             // The game over menu that is displayed when the player dies.
    public GameObject startText;                // The text informing the player to press a button to continue.
    public GameObject crosshair;                // The crosshair respresenting the centre of the screen.

    public AudioClip sceneChange;               // The audio that is played when the scene changes.

    private bool isPaused = false;              // Whether or not the game is currently paused.
    private bool returningToMainMenu = false;   // Whether or not the game is returning to the main menu.
    private GameObject openMenu;                // The menu that is currently open.
    private GameObject previousMenu;            // The menu that was open before the current one.

    /* Use this for initialization. */
    void Start()
    {
        if (!sceneController) { sceneController = FindObjectOfType<SceneController>(); }
        if (!audioController) { audioController = FindObjectOfType<AudioController>(); }
        if (!options) { options = FindObjectOfType<Options>(); }
        openMenu = mainMenu;
    }

    /* Update is called once per frame. */
    void Update()
    {
        if (sceneController.GetActiveSceneIndex() <= 1)
        {
            if (startText.activeInHierarchy && Input.anyKeyDown)
            {
                PlaySceneChangeAudio();
                startText.SetActive(false);
                ShowMenu(mainMenu);
            }
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
        options.ApplyPlayerPrefs();
        HideMenu(openMenu);
        ShowMenu(previousMenu);
    }

    /* Starts the game. */
    public void PlayGame()
    {
        // Unpause the game world to allow the scene transition.
        if (Time.timeScale == 0.0f)
        {
            Time.timeScale = 1.0f;
        }

        SceneController.AfterSceneLoad += MenuToGameScene;
        sceneController.FadeAndLoadScene("MeteorStrike");
    }

    /* Pauses the game. */
    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        ShowMenu(pauseMenu);
        isPaused = true;
        crosshair.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }

    /* Resumes the game. */
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        AttemptPlayerPrefUpdate();
        HideMenu(openMenu);
        isPaused = false;
        crosshair.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }

    /* Returns to the main menu. */
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1.0f;
        returningToMainMenu = true;
        SceneController.AfterSceneLoad += GameSceneToMainMenu;
        sceneController.FadeAndLoadScene("SplashScreen");
    }

    /* Quits the game. */
    public void QuitGame()
    {
        StartCoroutine(QuitAfterTime());
    }

    /* Play click audio. */
    public void PlayClickAudio()
    {
        audioController.GetAudioUI().Play();
    }

    /* Play scene change audio. */
    public void PlaySceneChangeAudio()
    {
        audioController.GetAudioUI().PlayOneShot(sceneChange);
    }

    /* Called after loading a new scene from the main or game over menu. */
    private void MenuToGameScene()
    {
        HideMenu(openMenu);
        Cursor.lockState = CursorLockMode.Locked;
        crosshair.SetActive(true);

        options.SetPlayerController();
        options.ApplyPlayerPrefs();

        PlayerCharacter.OnPlayerDeath += GameOver;
        SceneController.AfterSceneLoad -= MenuToGameScene;
    }

    /* Called after loading the main menu from a game scene. */
    private void GameSceneToMainMenu()
    {
        HideMenu(openMenu);
        ShowMenu(mainMenu);
        Cursor.lockState = CursorLockMode.None;
        crosshair.SetActive(false);
        returningToMainMenu = false;
        options.ApplyPlayerPrefs();
        SceneController.AfterSceneLoad -= GameSceneToMainMenu;
    }

    /* Called when the player dies to display the game over menu. */
    private void GameOver()
    {
        if (!returningToMainMenu)
        {
            Time.timeScale = 0.0f;
            isPaused = true;
            ShowMenu(gameOverMenu);
            Cursor.lockState = CursorLockMode.None;
            crosshair.SetActive(false);
            audioController.GetBackgroundAudio().Stop();
            PlayerCharacter.OnPlayerDeath -= GameOver;
        }
    }

    /* Apply any changes to the player prefs. */
    private void AttemptPlayerPrefUpdate()
    {
        if ((sceneController.GetActiveSceneIndex() > 1) && openMenu.name == "OptionsMenu")
        {
            // Returning from the options menu and we are in an active game scene so apply player prefs.
            options.ApplyPlayerPrefs();
        }
    }

    /* Quits the game (or Editor) after a couple of seconds. */
    private IEnumerator QuitAfterTime()
    {
        yield return new WaitForSeconds(0.5f);
        Application.Quit();

        // For exiting the editor.
        UnityEditor.EditorApplication.isPlaying = false;
    }
}