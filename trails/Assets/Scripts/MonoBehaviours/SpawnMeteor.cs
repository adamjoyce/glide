using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteor : MonoBehaviour
{
    public GameObject meteor;                   // The meteor prefab that will be spawned.
    public GameObject spawnZone;                // The area in which the meteors are able to spawn.
    public float spawnRate = 5.0f;              // The starting rate for how often meteors will spawn - every x seconds.

    private float nextSpawnTime = 0.0f;         // The next timestamp (from the beginning of the game) a meteor will be spawned.

    /* Use this for initialization. */
    private void Start()
    {
        // Attempt to get a reference to the spawn zone if it is not set.
        if (!spawnZone)
        {
            spawnZone = GameObject.Find("SpawnZone");
        }
    }

    /* Update is called once per frame. */
    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            Instantiate(meteor, RandomPointInZone.GetRandomPointInZone(spawnZone), Quaternion.identity);
            nextSpawnTime = Time.time + spawnRate;
        }
    }
}