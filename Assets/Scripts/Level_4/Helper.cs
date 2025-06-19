using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    [SerializeField] private GameObject GoodDialog;
    [SerializeField] private GameObject BadDialog;

    public void ShowBadDialog()
    {
        GoodDialog.SetActive(false);
        BadDialog.SetActive(true);
    }

    public void ShowGoodDialog()
    {
        GoodDialog.SetActive(true);
        BadDialog.SetActive(false);
    }
}
