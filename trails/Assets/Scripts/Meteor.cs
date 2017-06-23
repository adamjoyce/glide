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
    void Start()
    {
        impactZoneSize = impactZone.GetComponent<BoxCollider>().bounds.size;
        meteorMesh = transform.GetChild(0);

        // Assumes that the impact zone as equal dimensions and is centred at the world origin.
        float halfImpactZoneSize = impactZoneSize.x * 0.5f;
        targetImpactLocation = new Vector3(Random.Range(-halfImpactZoneSize, halfImpactZoneSize),
                                           Random.Range(-halfImpactZoneSize, halfImpactZoneSize),
                                           Random.Range(-halfImpactZoneSize, halfImpactZoneSize));
    }

    /* Update is called once per frame. */
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetImpactLocation, movementSpeed * Time.deltaTime);
        meteorMesh.RotateAround(meteorMesh.position, -Vector3.right, rotationSpeed * Time.deltaTime);
    }
}