using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsButton : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        foreach (var dialog in FindObjectsOfType<DialogueBox>())
        {
            Destroy(dialog.gameObject);
        }
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}