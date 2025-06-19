using Assets.Scripts.GlobalInformation;
using Assets.Scripts.Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel4 : MonoBehaviour
{
    void Start()
    {
        GlobalData.PreviousSceneName = "Level_4";
        var data = SaveManager.Load();
        data.currentLevel = 5;
        SaveManager.Save(data);
        SceneManager.LoadScene("Lobby");
    }
}
