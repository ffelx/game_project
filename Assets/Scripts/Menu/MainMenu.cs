using Assets.Scripts.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string _gameSceneName = "GameScene";

    public void Start()
    {
        CreateInventoryManager();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(_gameSceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Выход");
        Application.Quit();
    }

    private void CreateInventoryManager()
    {
        if (InventoryManager.Instance == null)
        {
            GameObject managerObject = new GameObject("InventoryManager");
            managerObject.AddComponent<InventoryManager>();
            Debug.Log("Count: " + InventoryManager.Instance.Count);
        }
    }
}
