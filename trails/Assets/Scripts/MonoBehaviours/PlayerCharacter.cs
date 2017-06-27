using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour, IDamageable<float>, IKillable
{
    public float maximumHealth = 100.0f;    // The starting health point cap of the character.

    public GameObject cannonBall;           // The gameobject that will be fired when space is pressed.
    public GameObject cannon;               // The gameobject the projectiles will originate from.
    public float shootForce = 0.0f;         // The initial force applied to the cannonball.
    public float shotDelay = 1.0f;          // The time that needs to pass before another shot can be taken.

    private float currentHealth = 0.0f;     // The current health points of the character at any given stage in tha game.
    private float nextFireTime = 0.0f;      // The next time increment at which another cannonball may be fired.

    /* Use this for initialization. */
    void Start()
    {
        currentHealth = maximumHealth;
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

    /* Dies if the damage taken reduces hits point to or below zero. */
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Player Dies.");
    }

    /* Returns the players current health value. */
    public float GetHealth()
    {
        return currentHealth;
    }

    /* Spawns and fires a cannonball. */
    private void FireCannonball()
    {
        GameObject projectile = Instantiate(cannonBall, cannon.transform.position, transform.rotation);
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * shootForce);
    }
}