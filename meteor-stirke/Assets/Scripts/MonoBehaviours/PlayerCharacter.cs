using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour, IDamageable<float>, IKillable
{
    public static event Action OnPlayerDeath;                               // Event delegate that is called when the player dies.

    public float maximumHealth = 100.0f;                                    // The starting health point cap of the character.

    public GameObject wispProjectile;                                       // The gameobject that will be fired when the fire key is pressed.
    public GameObject cannon;                                               // The gameobject the projectiles will originate from.
    public float shootForce = 0.0f;                                         // The initial force applied to the cannonball.
    public float shotDelay = 1.0f;                                          // The time that needs to pass before another shot can be taken.

    public Image damageImage;                                               // The image that flashes when damage is sustained.
    public Color damageFlashColor = new Color(1.0f, 0.0f, 0.0f, 0.75f);     // The colour that the damage image flashes when damage is taken.
    public float flashSpeed = 5.0f;                                         // The speed at which the damage image will fade.

    private float currentHealth = 0.0f;                                     // The current health points of the character at any given stage in tha game.
    private float nextFireTime = 0.0f;                                      // The next time increment at which another cannonball may be fired.

    public float CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    /* Use this for initialization. */
    void Start()
    {
        currentHealth = maximumHealth;
    }

    /* Update is called once per frame. */
    private void Update()
    {
        // Fire projectile.
        if ((Time.time >= nextFireTime) && Input.GetKeyDown(KeyCode.Space))
        {
            FireProjectile(wispProjectile);

            // Update when the cannon can next fire.
            nextFireTime = Time.time + shotDelay;
        }

        // Fades damage image.
        if (damageImage.color != Color.clear)
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
    }

    /* Dies if the damage taken reduces hits point to or below zero. */
    public void TakeDamage(float damageAmount)
    {
        // Update health and check for death.
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }

        // Flash damage feedback image.
        damageImage.color = damageFlashColor;
    }

    /* Called when the player's health reaches zero. */
    public void Die()
    {
        if (OnPlayerDeath != null)
            OnPlayerDeath();
    }

    /* Returns the players current health value. */
    public float GetHealth()
    {
        return currentHealth;
    }

    /* Spawns and fires a cannonball. */
    private void FireProjectile(GameObject projectileToFire)
    {
        GameObject projectile = Instantiate(projectileToFire, cannon.transform.position, transform.rotation);
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * shootForce);
    }
}