using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPointInZone : MonoBehaviour
{
    public static Vector3 GetRandomPointInZone(GameObject zone)
    {
        Vector3 zoneSize = zone.GetComponent<BoxCollider>().size;

        // Get the zone size in half dimensions for centred zone.
        float halfZoneSizeX = zoneSize.x * 0.5f;
        float halfZoneSizeY = zoneSize.y * 0.5f;
        float halfZoneSizeZ = zoneSize.z * 0.5f;

        // Calculate the random point location accounting for the zones position in world space.
        Vector3 point = new Vector3(Random.Range(-halfZoneSizeX, halfZoneSizeX) + zone.transform.position.x,
                                    Random.Range(-halfZoneSizeY, halfZoneSizeY) + zone.transform.position.y,
                                    Random.Range(-halfZoneSizeZ, halfZoneSizeZ) + zone.transform.position.z);

        return point;
    }
}