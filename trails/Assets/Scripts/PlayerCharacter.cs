using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public GameObject cannonBall;           // The gameobject that will be fired when space is pressed.
    public GameObject cannon;               // The gameobject the projectiles will originate from.
    public float shootForce = 0.0f;         // The initial force applied to the cannonball.
    public float shotDelay = 1.0f;          // The time that needs to pass before another shot can be taken.

    private float nextFireTime = 0.0f;      // The next time increment at which another cannonball may be fired.

    /* Use this for initialization. */
    void Start()
    {

    }

    /* Update is called once per frame. */
    private void Update()
    {
        if ((Time.time >= nextFireTime) && Input.GetKeyDown(KeyCode.Space))
        {
            FireCannonball();

            // Update when the cannon can next fire.
            nextFireTime = Time.time + shotDelay;
        }
    }

    /* Spawns and fires a cannonball. */
    private void FireCannonball()
    {
        GameObject projectile = Instantiate(cannonBall, cannon.transform.position, transform.rotation);
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * shootForce);
    }
}