using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public Slider sensitivity;                             // The slider that determines mouse sensitivity.
    public Slider masterVolume;                            // The slider that determines the master volume for the game.
    public Slider musicVolume;                             // The slider that determines the music volume for the game.

    private static PlayerController playerController;      // The controller attached to the player in the game scene.
    private static AudioController audioController;        // The controller that manages the game audio levels.

    /* Use for initilisation. */
    private void Start()
    {
        sensitivity.value = PlayerPrefs.GetFloat("MouseSensitivity");
        masterVolume.value = PlayerPrefs.GetFloat("MasterVolume");
        musicVolume.value = PlayerPrefs.GetFloat("MusicVolume");
        StartCoroutine(SetAudioLate());
    }

    /* Sets the player controller in a game scene. */
    public static void SetPlayerController()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    /* Sets the audio controller in a game scene. */
    public static void SetAudioController()
    {
        audioController = FindObjectOfType<AudioController>();
    }

    /* Applies the player prefs for the game. */
    public static void ApplyPlayerPrefs()
    {
        if (playerController) { playerController.SetMouseSensitivity(PlayerPrefs.GetFloat("MouseSensitivity")); }
        audioController.SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume"));
        audioController.SetBackgroundVolume(PlayerPrefs.GetFloat("MusicVolume"));
    }

    /* Sets a the mouse sensitivity as a player pref. */
    public void SetMouseSensitivity()
    {
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivity.value);
    }

    /* Sets the master volume as a player pref. */
    public void SetMasterVolume()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolume.value);
        ApplyPlayerPrefs();
    }

    /* Sets the music volume as a player pref. */
    public void SetMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume.value);
        ApplyPlayerPrefs();
    }

    /* Sets the initial audio controller after the splash screen scene is loaded. */
    private IEnumerator SetAudioLate()
    {
        yield return new WaitUntil(() => SceneManager.GetActiveScene().buildIndex > 0);
        SetAudioController();
    }
}