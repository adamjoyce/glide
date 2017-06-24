using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public GameObject impactZone;           // The centre of the area the meteor will aim towards.
    public float movementSpeed = 5.0f;      // The speed the meteor will travel at.
    public float rotationSpeed = 90.0f;     // The speed at which the meteor rotates.

    private Transform meteorMesh;          // The meteor's mesh that is visible in the scene.
    private Vector3 impactZoneSize;         // The dimensions of the box collider on the impact zone gameobject.
    private Vector3 targetImpactLocation;   // The target location the meteor will aim for.

    /* Use this for initialization. */
    private void Start()
    {
        impactZoneSize = impactZone.GetComponent<BoxCollider>().bounds.size;
        meteorMesh = transform.GetChild(0);

        // Assumes that the impact zone as equal dimensions.
        float halfImpactZoneSize = impactZoneSize.x * 0.5f;
        targetImpactLocation = new Vector3(Random.Range(-halfImpactZoneSize, halfImpactZoneSize) + impactZone.transform.position.x,
                                           Random.Range(-halfImpactZoneSize, halfImpactZoneSize) + impactZone.transform.position.y,
                                           Random.Range(-halfImpactZoneSize, halfImpactZoneSize) + impactZone.transform.position.z);
        transform.LookAt(targetImpactLocation);
    }

    /* Update is called once per frame. */
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetImpactLocation, movementSpeed * Time.deltaTime);
        meteorMesh.RotateAround(meteorMesh.position, -Vector3.right, rotationSpeed * Time.deltaTime);
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