using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispProjectile : MonoBehaviour
{
    public float destructionDelay = 5.0f;           // The amount of time in seconds before the projectile self-destructs.

    /* Use this for initialization. */
    void Start()
    {
        Destroy(gameObject, destructionDelay);
    }
}