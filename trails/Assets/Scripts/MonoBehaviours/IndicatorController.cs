using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorController : MonoBehaviour
{
    public MeteorManager meteorManager;                                          // The script that manages the active meteors.
    public GameObject targetIndicatorPrefab;                                     // The prefab for the target UI GameObject element displayed when the object is on-screen.
    public GameObject arrowIndicatorPrefab;                                      // The prefab for the arrow UI GameObject element displayed when the object is off-screen.

    private List<GameObject> targetIndicators = new List<GameObject>();          // The pool of target indicators.
    private List<GameObject> arrowIndicators = new List<GameObject>();           // The pool of arrow indicators.
    private int currentTargetIndicatorIndex = 0;                                 // The first unassigned target indicator.
    private int currentArrowIndicatorIndex = 0;                                  // The first unassigned arrow indicator.

    private GameObject spawnZone;                                                // Object spawn zone to determine maximum distance from the player for arrow scaling.
    private BoxCollider spawnZoneCollider;                                       // The collider for the spawn zone.
    private const float arrowScaleMultipler = 5;                                 // The multipler for arrow scaling.

    /* Use for intialisation. */
    private void Start()
    {
        spawnZone = GameObject.Find("SpawnZone");
        if (spawnZone)
            spawnZoneCollider = spawnZone.GetComponent<BoxCollider>();
    }

    /* Update is called once per frame after Update functions have been called. */
    private void LateUpdate()
    {
        DrawIndicators();
    }

    /* Handles drawing both the on-screen target and off-screen arrow indicators. */
    private void DrawIndicators()
    {
        ResetIndicatorPools();

        List<GameObject> meteors = meteorManager.GetMeteors();
        for (int i = 0; i < meteors.Count; ++i)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(meteors[i].transform.position);
            if (screenPos.z > 0 &&
                screenPos.x > 0 && screenPos.x < Screen.width &&
                screenPos.y > 0 && screenPos.y < Screen.height)
            {
                // The object is located on-screen.
                GameObject targetIndicator = GetTargetIndicator();
                targetIndicator.transform.position = screenPos;
            }
            else
            {
                // The object is loacted off-screen.
                // Flips the screen position of objects when they are behind the camera.
                //if (screenPos.z < 0)
                //{
                //    screenPos *= -1;
                //}

                // Translate screen coordinates to make the screen centre the origin.
                Vector3 screenOrigin = new Vector3(Screen.width, Screen.height, 0) * 0.5f;
                screenPos -= screenOrigin;

                // Find the angle from the origin to the mouse position.
                float angle = Mathf.Atan2(screenPos.y, screenPos.x);
                angle -= 90 * Mathf.Deg2Rad;
                float angleSin = -Mathf.Sin(angle);
                float angleCos = Mathf.Cos(angle);

                //screenPos = screenOrigin + new Vector3(angleSin * 180, angleCos * 180, 0);

                // Using the slope intercept (y = mx + b).
                float gradient = angleCos / angleSin;

                // Adds a small gap between the arrows and the edge fo the screen.
                Vector3 screenBounds = screenOrigin * 0.9f;

                // Determine the vertical direction for the arrow.
                if (angleCos > 0)
                {
                    // The arrow is in the top half of the screen.
                    screenPos = new Vector3(screenBounds.y / gradient, screenBounds.y, 0);
                }
                else
                {
                    // The arrow is in the bottom half of the screen.
                    screenPos = new Vector3(-screenBounds.y / gradient, -screenBounds.y, 0);
                }

                // Determine the horizontal direction for the arrow.
                if (screenPos.x > screenBounds.x)
                {
                    // The arrow is on the right side of the screen.
                    screenPos = new Vector3(screenBounds.x, screenBounds.x * gradient, 0);
                }
                else if (screenPos.x < -screenBounds.x)
                {
                    // The arrow is on the left side of the screen.
                    screenPos = new Vector3(-screenBounds.x, -screenBounds.x * gradient, 0);
                }

                // Remove the coordinate translation.
                screenPos += screenOrigin;

                // Set an arrow indicator.
                GameObject arrowIndicator = GetArrowIndicator();
                arrowIndicator.transform.position = screenPos;
                arrowIndicator.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
                ScaleArrow(meteors[i], ref arrowIndicator);
            }
        }

        CullIndicatorPools();
    }

    /* Returns a target indicator UI GameObject from the target indicator pool. */
    private GameObject GetTargetIndicator()
    {
        GameObject targetIndicator;
        if (currentTargetIndicatorIndex < targetIndicators.Count)
        {
            // Use the existing target indicator.
            targetIndicator = targetIndicators[currentTargetIndicatorIndex];
        }
        else
        {
            // Create a new target indicator.
            targetIndicator = Instantiate(targetIndicatorPrefab);
            targetIndicator.transform.SetParent(transform);
            targetIndicators.Add(targetIndicator);
        }

        currentTargetIndicatorIndex++;
        return targetIndicator;
    }

    /* Returns a arrow indicator UI GameObject from the arrow indicator pool. */
    private GameObject GetArrowIndicator()
    {
        GameObject arrowIndicator;
        if (currentArrowIndicatorIndex < arrowIndicators.Count)
        {
            // Use the existing arrow indicator.
            arrowIndicator = arrowIndicators[currentArrowIndicatorIndex];
        }
        else
        {
            // Create a new arrow indicator.
            arrowIndicator = Instantiate(arrowIndicatorPrefab);
            arrowIndicator.transform.SetParent(transform);
            arrowIndicators.Add(arrowIndicator);
        }

        currentArrowIndicatorIndex++;
        return arrowIndicator;
    }

    /* Resets the indicators to be reassigned from the first index. */
    private void ResetIndicatorPools()
    {
        currentTargetIndicatorIndex = 0;
        currentArrowIndicatorIndex = 0;
    }

    /* Culls unused indicator objects. */
    private void CullIndicatorPools()
    {
        // Target Indicators.
        while (targetIndicators.Count > currentTargetIndicatorIndex)
        {
            GameObject targetIndicator = targetIndicators[targetIndicators.Count - 1];
            targetIndicators.Remove(targetIndicator);
            Destroy(targetIndicator);
        }

        // Arrow Indicators.
        while (arrowIndicators.Count > currentArrowIndicatorIndex)
        {
            GameObject arrowIndicator = arrowIndicators[arrowIndicators.Count - 1];
            arrowIndicators.Remove(arrowIndicator);
            Destroy(arrowIndicator);
        }
    }

    /* Scales the arrow indicator based on object distance to player. */
    private void ScaleArrow(GameObject obj, ref GameObject arrowIndicator)
    {
        float distance = (Camera.main.transform.position - obj.transform.position).magnitude;
        float distanceNorm = distance / (spawnZone.transform.position.z + (spawnZoneCollider.size.z * 0.5f));
        float scaleValue = (1 - distanceNorm) * arrowScaleMultipler;
        arrowIndicator.transform.localScale = new Vector3(scaleValue, scaleValue, 0);
    }
}