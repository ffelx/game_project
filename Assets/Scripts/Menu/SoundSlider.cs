using Assets.Scripts.GlobalInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider slider;

    private const string MusicVolumeParam = "MusicVolume";
    private const string SFXVolumeParam = "SFXVolume";
    private const string MasterVolumeParam = "MasterVolume";

    void Start()
    {
        if (slider != null)
        {
            slider.onValueChanged.AddListener(OnSliderChanged);
            OnSliderChanged(slider.value);
        }
    }

    void OnSliderChanged(float value)
    {
        float volumeDb = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;

        _mixer.SetFloat(MasterVolumeParam, volumeDb);
        GlobalData.Volume = value;   

        Debug.Log($"Slider value: {value}, volume dB: {volumeDb}");
    }
}
