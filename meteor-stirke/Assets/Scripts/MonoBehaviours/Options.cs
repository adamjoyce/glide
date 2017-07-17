using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public Slider sensitivity;                          // The slider that determines mouse sensitivity.
    public Slider masterVolume;                         // The slider that determines the master volume for the game.
    public Slider musicVolume;                          // The slider that determines the music volume for the game.

    private PlayerController playerController;          // The controller attached to the player in the game scene.
    [SerializeField]
    private AudioController audioController;            // The audio controller that manages the games audio.

    /* Use for initilisation. */
    private void Start()
    {
        audioController = FindObjectOfType<AudioController>();
        sensitivity.value = PlayerPrefs.GetFloat("MouseSensitivity", 1);
        masterVolume.value = PlayerPrefs.GetFloat("MasterVolume", 100);
        musicVolume.value = PlayerPrefs.GetFloat("MusicVolume", 50);
    }

    /* Sets the player controller in a game scene. */
    public void SetPlayerController()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    /* Applies the player prefs for the game. */
    public void ApplyPlayerPrefs()
    {
        if (playerController)
            playerController.SetMouseSensitivity(PlayerPrefs.GetFloat("MouseSensitivity"));

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
        audioController.SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume"));
    }

    /* Sets the music volume as a player pref. */
    public void SetMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume.value);

        // The initial background audio source may not have been set yet as it resides in the splash screen scene.
        if (audioController.HasBackgroundAudio())
            audioController.SetBackgroundVolume(PlayerPrefs.GetFloat("MusicVolume"));
    }
}