using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour, IDamageable<float>, IKillable
{
    public MeteorManager meteorManager;     // The script that manages the active meteors.
    public GameObject impactZone;           // The centre of the area the meteor will aim towards.
    public float movementSpeed = 5.0f;      // The speed the meteor will travel at.
    public float maximumHealth = 100.0f;    // The starting health of a meteor.

    private Vector3 impactZoneSize;         // The dimensions of the box collider on the impact zone gameobject.
    private Vector3 targetImpactLocation;   // The target location the meteor will aim for.
    private float currentHealth = 0.0f;     // The current health of the meteor at any given moment during the game.

    /* Use this for initialization. */
    private void Start()
    {
        // Fetch references if not set.
        if (!meteorManager) { meteorManager = GameObject.Find("MeteorManager").GetComponent<MeteorManager>(); }
        if (!impactZone) { impactZone = GameObject.Find("ImpactZone"); }

        // Generate the target location within the impact zone and face the meteor towards it.
        targetImpactLocation = RandomPointInZone.GetRandomPointInZone(impactZone);
        transform.LookAt(targetImpactLocation);

        currentHealth = maximumHealth;
    }

    /* Update is called once per frame. */
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetImpactLocation, movementSpeed * Time.deltaTime);
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
        meteorManager.RemoveMeteor(gameObject);
        Destroy(gameObject);
    }

    /* What happens when something collides with this object. */
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cannonball")
        {
            Destroy(other.gameObject);
            Die();
        }
    }
}