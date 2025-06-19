using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupInitializer2 : MonoBehaviour
{
    [SerializeField] private GameObject popupPrefab;
    private void Awake()
    {
        popupPrefab.SetActive(true);
    }
}
