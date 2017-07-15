using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject meteorManager;                    // The object that managers the meteor spawns.
    public int difficultyInterval = 20;                 // The amount of time before the difficulty is increased.

    private Text timer;                                 // The timer that displays how much time has passed.
    private Animator anim;                              // The animator for the difficulty scaling effect.
    private float nextDifficultyIncrease = 0.0f;        // The next time stamp (from the beginning of the scene) a diffuclty increase will occur. 

    /* Use this for initialization. */
    void Start()
    {
        timer = GetComponent<Text>();
        anim = GetComponent<Animator>();

        if (!meteorManager)
            meteorManager = GameObject.Find("MeteorManager");

        nextDifficultyIncrease = difficultyInterval;
    }

    /* Update is called once per frame. */
    void Update()
    {
        // Check if enough time has passed to increase the difficulty.
        if (Time.timeSinceLevelLoad >= nextDifficultyIncrease)
        {
            bool difficultyIncreased = meteorManager.GetComponent<MeteorManager>().IncreaseDifficulty();
            if (difficultyIncreased)
            {
                anim.SetTrigger("DifficultyIncrease");
                nextDifficultyIncrease = Time.timeSinceLevelLoad + difficultyInterval;
            }
        }

        // Update the on-screen timer.
        int time = (int)Time.timeSinceLevelLoad;
        timer.text = time.ToString();
    }
}