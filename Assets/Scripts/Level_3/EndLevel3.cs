using Assets.Scripts.GlobalInformation;
using Assets.Scripts.Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel3 : MonoBehaviour
{
    void Start()
    {
        GlobalData.PreviousSceneName = "Level_3";
        var data = SaveManager.Load();
        data.currentLevel = 4;
        SaveManager.Save(data);
        SceneManager.LoadScene("Lobby");
    }
}
