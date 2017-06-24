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
        impactZoneSize = impactZone.GetComponent<BoxCollider>().bounds.size;

        // Half values to correctly calculate a position within the impact zone bounds.
        float halfImpactZoneSizeX = impactZoneSize.x * 0.5f;
        float halfImpactZoneSizeY = impactZoneSize.y * 0.5f;
        float halfImpactZoneSizeZ = impactZoneSize.z * 0.5f;

        // Generate the target location within the impact zone and face the meteor towards it.
        targetImpactLocation = new Vector3(Random.Range(-halfImpactZoneSizeX, halfImpactZoneSizeX) + impactZone.transform.position.x,
                                           Random.Range(-halfImpactZoneSizeY, halfImpactZoneSizeY) + impactZone.transform.position.y,
                                           Random.Range(-halfImpactZoneSizeZ, halfImpactZoneSizeZ) + impactZone.transform.position.z);
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