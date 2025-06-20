using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCellSound : MonoBehaviour
{
    private AudioManager _audioManager;
    void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    public void PlaySound()
    {
        _audioManager.PLayTreeSound();  
    }
}
