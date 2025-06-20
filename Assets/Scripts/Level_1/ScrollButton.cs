using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ScrollButton : MonoBehaviour
{
    private Button _button;
    private AudioManager _audioManager;

    void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
        _audioManager = FindObjectOfType<AudioManager>();
    }

    void OnClick()
    {
        _audioManager.PlayScrollClickSound();
    }

    void OnDestroy()
    {
        if (_button != null)
        {
            _button.onClick.RemoveListener(OnClick);
        }
    }
}
