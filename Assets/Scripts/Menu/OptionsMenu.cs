using Assets.Scripts.GlobalInformation;
using Assets.Scripts.Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuButton;
    [SerializeField] private GameObject _exitButton;

    [SerializeField] private Slider _slider;
    void Start()
    {
        PlayerData data = SaveManager.Load();
        _slider.value = data.volume;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            _mainMenuButton.SetActive(false);
            _exitButton.SetActive(false);
            return;
        }
        _mainMenuButton.SetActive(true);
        _exitButton.SetActive(true);
    }


    void OnEnable()
    {
        transform.SetAsLastSibling();
    }

    public void SaveOptions()
    {
        PlayerData data = SaveManager.Load();
        data.volume = _slider.value;
        SaveManager.Save(data);
    }
}
