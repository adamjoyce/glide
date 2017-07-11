using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorController : MonoBehaviour
{
    public Image targetIndicatorPrefab;                                 // The prefab for the target UI Image element displayed when the object is on-screen.
    public Image arrowIndicatorPrefab;                                  // The prefab for the arrow UI Image element displayed when the object is off-screen.

    private List<Image> targetIndicators = new List<Image>();           // The pool of target indicators.
    private List<Image> arrowIndicators = new List<Image>();            // The pool of arrow indicators.
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
    }

    /* Resets the indicators to be reassigned from the first index. */
    private void ResetIndicatorPools()
    {
        currentTargetIndicatorIndex = 0;
        currentArrowIndicatorIndex = 0;
    }
}