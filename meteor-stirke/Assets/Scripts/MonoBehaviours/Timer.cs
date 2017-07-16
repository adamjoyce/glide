using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject meteorManager;                    // The object that managers the meteor spawns.

    private Text timerText;                             // The timer UI element that displays how much time has passed.
    private Animator anim;                              // The animator for the difficulty scaling effect.
    private AudioSource audio;                          // The audio source holding the difficulty increase effect.
    private float nextDifficultyIncrease = 0.0f;        // The next time stamp (from the beginning of the scene) a diffuclty increase will occur. 
    private float startTime = 0.0f;                     // The timestamp at which the level began.
    private int difficultyInterval = 0;                 // The amount of time before the difficulty is increased.

    /* Use this for initialization. */
    void Start()
    {
        timerText = GetComponent<Text>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();

        if (!meteorManager)
            meteorManager = GameObject.Find("MeteorManager");

        if (meteorManager)
        {
            difficultyInterval = meteorManager.GetComponent<MeteorManager>().difficultyInterval;
            nextDifficultyIncrease = difficultyInterval;
        }

        startTime = Time.time;
    }

    /* Update is called once per frame. */
    void Update()
    {
        // Check if enough time has passed to increase the difficulty.
        float timer = Time.time - startTime;
        if (timer >= nextDifficultyIncrease)
        {
            bool difficultyIncreased = meteorManager.GetComponent<MeteorManager>().IncreaseDifficulty();
            if (difficultyIncreased)
            {
                anim.SetTrigger("DifficultyIncrease");
                audio.Play();
                nextDifficultyIncrease = timer + difficultyInterval;
            }
        }

        // Update the on-screen timer.
        int integerTime = (int)timer;
        timerText.text = integerTime.ToString();
    }
}