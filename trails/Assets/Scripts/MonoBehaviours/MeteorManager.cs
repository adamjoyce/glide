using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorManager : MonoBehaviour
{
    public GameObject meteor;                                       // The meteor prefab that will be spawned.
    public GameObject spawnZone;                                    // The area in which the meteors are able to spawn.
    public float spawnRate = 5.0f;                                  // The starting rate for how often meteors will spawn - every x seconds.

    [SerializeField]
    private List<GameObject> meteors = new List<GameObject>();      // The list of all active meteors.
    private float nextSpawnTime = 0.0f;                             // The next timestamp (from the beginning of the game) a meteor will be spawned.

    /* Use this for initialization. */
    private void Start()
    {
        // Attempt to get a reference to the spawn zone if it is not set.
        if (!spawnZone) { spawnZone = GameObject.Find("SpawnZone"); }
    }

    /* Update is called once per frame. */
    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            GameObject newMeteor = Instantiate(meteor, RandomPointInZone.GetRandomPointInZone(spawnZone), Quaternion.identity);
            meteors.Add(newMeteor);
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    /* Returns the list of active meteors. */
    public List<GameObject> GetMeteors()
    {
        return meteors;
    }

    /* Removes the given meteor from the list of active meteors. */
    public void RemoveMeteor(GameObject meteorToRemove)
    {
        meteors.Remove(meteorToRemove);
    }
}