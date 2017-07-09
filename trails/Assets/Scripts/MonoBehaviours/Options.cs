using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider sensitivity;                             // The slider that determines mouse sensitivity.

    private static PlayerController playerController;      // The controller attached to the player in the game scene.

    /* Sets the player controller in a game scene. */
    public static void SetPlayerController()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    /* Applies the player prefs for the game. */
    public static void ApplyPlayerPrefs()
    {
        playerController.SetMouseSensitivity(PlayerPrefs.GetFloat("MouseSensitivity"));
    }

    /* Sets a the mouse sensitivity as a player pref. */
    public void SetMouseSensitivity()
    {
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivity.value);
    }
}