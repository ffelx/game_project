using Assets.Scripts.GlobalInformation;
using Assets.Scripts.Items;
using Assets.Scripts.Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private AudioMixer _mixer;

    public void Start()
    {
        Debug.Log(Application.persistentDataPath);
        LoadSave();
        CreateInventoryManager();
    }

    public void LoadSave()
    {
        PlayerData data = SaveManager.Load();
        GlobalData.PreviousSceneName = "Level_" + (data.currentLevel - 1);
        GlobalData.Volume = data.volume;
        _mixer.SetFloat("MasterVolume", Mathf.Log10(GlobalData.Volume) * 20);
        //_mixer.SetFloat("SFX", Mathf.Log10(GlobalData.Volume) * 20);
        _slider.value = data.volume;
    }

    public void PlayGame()
    {
        Debug.Log(GlobalData.PreviousSceneName);
        if (GlobalData.PreviousSceneName == "Level_0")
        {
            SceneManager.LoadScene("Level_1");
            return;
        }
        SceneManager.LoadScene("Lobby");
    }

    public void ExitGame()
    {
        Debug.Log("�����");
        Application.Quit();
    }

    private void CreateInventoryManager()
    {
        if (InventoryManager.Instance == null)
        {
            GameObject managerObject = new GameObject("InventoryManager");
            managerObject.AddComponent<InventoryManager>();
        }
    }
}
