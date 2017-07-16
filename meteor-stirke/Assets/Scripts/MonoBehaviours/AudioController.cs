using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public AudioSource audioUI;                 // The audio source for the UI menu elements.

    [SerializeField]
    private AudioSource audioBackground;        // The audio source for the background tracks.

    /* Use this for initialization. */
    void Start()
    {
        if (!audioUI) { audioUI = GameObject.Find("Canvas").GetComponent<AudioSource>(); }

        StartCoroutine(SetAudioLate());
        SceneController.AfterSceneLoad += SetBackgroundAudio;
    }

    /* Update is called once per frame. */
    void Update()
    {

    }

    /* Returns the UI audio source. */
    public AudioSource GetAudioUI()
    {
        return audioUI;
    }

    /* Returns the background audio source. */
    public AudioSource GetBackgroundAudio()
    {
        return audioBackground;
    }

    /* Returns true id the background audio source has been referenced. */
    public bool HasBackgroundAudio()
    {
        if (!audioBackground)
            return false;
        return true;
    }

    /* Gets the background audio source. */
    private void SetBackgroundAudio()
    {
        audioBackground = GameObject.Find("BackgroundAudio").GetComponent<AudioSource>();
    }

    /* Sets the master volume level. */
    public void SetMasterVolume(float volumeLevel)
    {
        float normalisedVolume = NormaliseValue(volumeLevel, 0, 100);
        AudioListener.volume = normalisedVolume;
    }

    /* Sets the background music's volume level. */
    public void SetBackgroundVolume(float volumeLevel)
    {
        float normalisedVolume = NormaliseValue(volumeLevel, 0, 100);
        audioBackground.volume = normalisedVolume;
    }

    /* Sets the initial background audio source after the splash screen scene is loaded. */
    private IEnumerator SetAudioLate()
    {
        yield return new WaitUntil(() => SceneManager.GetActiveScene().buildIndex > 0);
        SetBackgroundAudio();

        // Set the initial player pref volume here instead of Options.cs due to late loading.
        SetBackgroundVolume(PlayerPrefs.GetFloat("MusicVolume"));
    }

    /* Normlises a given value to fall betwen min and max. */
    private float NormaliseValue(float value, float min, float max)
    {
        float normalisedValue = (value - min) / (max - min);
        return normalisedValue;
    }

    /* Behaviour for when the attached GameObject is disabled. */
    private void OnDisable()
    {
        SceneController.AfterSceneLoad -= SetBackgroundAudio;
    }
}