using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject cannonBall;           // The gameobject that will be fired when space is pressed.
    public GameObject cannon;               // The gameobject the projectiles will originate from.
    public float shootForce = 0.0f;         // The initial force applied to the cannonball.

    /* Update is called once per frame. */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject projectile = Instantiate(cannonBall, cannon.transform.position, transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * shootForce);
        }
    }
}