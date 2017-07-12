using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorController : MonoBehaviour
{
    public MeteorManager meteorManager;                                 // The script that manages the active meteors.
    public GameObject targetIndicatorPrefab;                                 // The prefab for the target UI GameObject element displayed when the object is on-screen.
    public GameObject arrowIndicatorPrefab;                                  // The prefab for the arrow UI GameObject element displayed when the object is off-screen.

    [SerializeField]
    private List<GameObject> targetIndicators = new List<GameObject>();           // The pool of target indicators.
    private List<GameObject> arrowIndicators = new List<GameObject>();            // The pool of arrow indicators.
    [SerializeField]
    private int currentTargetIndicatorIndex = 0;                        // The first unassigned target indicator.
    private int currentArrowIndicatorIndex = 0;                         // The first unassigned arrow indicator.

    /* Update is called once per frame after Update functions have been called. */
    private void LateUpdate()
    {
        DrawIndicators();
    }

    /**/
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
                // Get indicator and set location...
                GameObject targetIndicator = GetTargetIndicator();
                targetIndicator.transform.position = screenPos;
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
    }
}