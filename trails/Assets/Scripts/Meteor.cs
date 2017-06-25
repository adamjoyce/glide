using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public GameObject impactZone;           // The centre of the area the meteor will aim towards.
    public float movementSpeed = 5.0f;      // The speed the meteor will travel at.

    private Vector3 impactZoneSize;         // The dimensions of the box collider on the impact zone gameobject.
    private Vector3 targetImpactLocation;   // The target location the meteor will aim for.

    /* Use this for initialization. */
    private void Start()
    {
        // Attempt to get a reference to the impact zone if it is not set.
        if (!impactZone)
        {
            impactZone = GameObject.Find("ImpactZone");
        }

        // Generate the target location within the impact zone and face the meteor towards it.
        targetImpactLocation = RandomPointInZone.GetRandomPointInZone(impactZone);
        transform.LookAt(targetImpactLocation);
    }

    /* Update is called once per frame. */
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetImpactLocation, movementSpeed * Time.deltaTime);
    }

    /* What happens when something collides with this object. */
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cannonball")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}