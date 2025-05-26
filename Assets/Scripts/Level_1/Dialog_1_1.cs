using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_1_1 : DialogueTriggerScriptActive
{
    [SerializeField] private Image imageToFlip;
    [SerializeField] private GameObject dialogObj;


    protected override void AfterDialogue()
    {
        Debug.Log("After dialogue 1 1");

        if (imageToFlip != null)
        {
            Transform imgTransform = imageToFlip.transform;
            Vector3 scale = imgTransform.localScale;
            scale = new Vector3(-scale.x, scale.y, scale.z);
            imgTransform.localScale = scale;
        }
        dialogObj.SetActive(true);
    }
}
