using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBoxDeactivator : MonoBehaviour
{
    [SerializeField] GameObject _objectToActivate;
    private void Start()
    {
        DialogBoxesSetActiveFalse();
        _objectToActivate.SetActive(true);
    }

    public void DialogBoxesSetActiveFalse()
    {
        foreach (var dialog in FindObjectsOfType<DialogueBox>())
        {
            dialog.gameObject.SetActive(false);
        }
    }
}
