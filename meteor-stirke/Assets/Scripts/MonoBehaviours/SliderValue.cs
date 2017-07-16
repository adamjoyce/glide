using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{
    /* Updates the displayed value based on the slider value. */
    public void SetTextValueFromSlider(float sliderValue)
    {
        GetComponentInChildren<Text>().text = sliderValue.ToString();
    }
}