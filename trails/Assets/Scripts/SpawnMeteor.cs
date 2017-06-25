using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteor : MonoBehaviour
{
    public GameObject meteor;                   // The meteor prefab that will be spawned.
    public GameObject spawnZone;                // The area in which the meteors are able to spawn.
    public float spawnRate = 5.0f;              // The starting rate for how often meteors will spawn - every x seconds.
          
    private Vector3 spawnZoneSize;              // The dimensions of the spawn zone.
    private float halfspawnZoneSizeX = 0.0f;    // Half the X dimension of the spawn zone.
    private float halfspawnZoneSizeY = 0.0f;    // Half the Y dimension of the spawn zone.
    private float halfspawnZoneSizeZ = 0.0f;    // Half the Z dimension of the spawn zone.
    private float nextSpawnTime = 0.0f;         // The next timestamp (from the beginning of the game) a meteor will be spawned.

    /* Use this for initialization. */
    private void Start()
    {
        spawnZoneSize = spawnZone.GetComponent<BoxCollider>().size;
        halfspawnZoneSizeX = spawnZoneSize.x * 0.5f;
        halfspawnZoneSizeY = spawnZoneSize.y * 0.5f;
        halfspawnZoneSizeZ = spawnZoneSize.z * 0.5f;
    }

    /* Update is called once per frame. */
    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            Instantiate(meteor, GetRandomSpawnLocation(), Quaternion.identity);
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    /* Returns a random location for a meteor to be spawned at within the spawnZone. */
    /* TODO: Abstract this for use in this class and Meteor.cs. */
    private Vector3 GetRandomSpawnLocation()
    {
        Vector3 spawnLocation = new Vector3(Random.Range(-halfspawnZoneSizeX, halfspawnZoneSizeX) + spawnZone.transform.position.x,
                                            Random.Range(-halfspawnZoneSizeY, halfspawnZoneSizeY) + spawnZone.transform.position.y,
                                            Random.Range(-halfspawnZoneSizeZ, halfspawnZoneSizeZ) + spawnZone.transform.position.z);
        return spawnLocation;
    }
}