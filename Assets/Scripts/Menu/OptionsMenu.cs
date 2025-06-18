using Assets.Scripts.GlobalInformation;
using Assets.Scripts.Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    void Start()
    {
        PlayerData data = SaveManager.Load();
        _slider.value = data.volume;
    }

    public void SaveOptions()
    {
        PlayerData data = SaveManager.Load();
        data.volume = _slider.value;
        SaveManager.Save(data);
    }
}
