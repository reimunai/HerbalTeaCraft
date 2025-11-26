using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDControlTest : MonoBehaviour
{
    public Slider slider;

    public void ChangeSliderValue(float value)
    {
        slider.value = value;
    }

}
