using UnityEngine;

public class DialogueTriggerScriptActive : DialogueTrigger
{
    void Start()
    {
        TriggerDialogue();
    }

    protected override void AfterDialogue()
    {
        Debug.Log("After dialogue active");
    }
}