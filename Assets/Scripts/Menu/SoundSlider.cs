using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        if (slider != null)
        {
            slider.onValueChanged.AddListener(OnSliderChanged);
            Debug.Log("123");
        }
    }

    void OnSliderChanged(float value)
    {
        Debug.Log(value);
    }
}
