using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBoxDeactivator : MonoBehaviour
{
    private void Start()
    {
        DialogBoxesSetActiveFalse();
    }

    public void DialogBoxesSetActiveFalse()
    {
        foreach (var dialog in FindObjectsOfType<DialogueBox>())
        {
            dialog.gameObject.SetActive(false);
        }
    }
}
