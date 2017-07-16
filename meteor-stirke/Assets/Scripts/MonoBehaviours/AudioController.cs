using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public AudioSource audioUI;                 // The audio source for the UI menu elements.

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
    public AudioSource GetAudioBackground()
    {
        return audioBackground;
    }

    /* Sets the master volume level. */
    public void SetMasterVolume(float volumeLevel)
    {
        float normalisedVolume = NormaliseValue(volumeLevel, 0, 1);
        AudioListener.volume = normalisedVolume;
    }

    /* Sets the background music's volume level. */
    public void SetBackgroundVolume(float volumeLevel)
    {
        float normalisedVolume = NormaliseValue(volumeLevel, 0, 1);
        audioBackground.volume = volumeLevel;
    }

    /* Gets the background audio source. */
    private void SetBackgroundAudio()
    {
        audioBackground = GameObject.Find("BackgroundAudio").GetComponent<AudioSource>();
    }

    /* Sets the initial background audio source after the splash screen scene is loaded. */
    private IEnumerator SetAudioLate()
    {
        yield return new WaitUntil(() => SceneManager.GetActiveScene().buildIndex > 0);
        if (!audioBackground) { SetBackgroundAudio(); }
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