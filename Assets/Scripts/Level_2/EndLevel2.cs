using Assets.Scripts.GlobalInformation;
using Assets.Scripts.Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel2 : MonoBehaviour
{
    void Start()
    {
        GlobalData.PreviousSceneName = "Level_2";
        var data = SaveManager.Load();
        data.currentLevel = 3;
        SaveManager.Save(data);
        SceneManager.LoadScene("Lobby");
    }
}
