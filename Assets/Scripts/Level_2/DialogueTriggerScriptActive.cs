using UnityEngine;

public class DialogueTriggerScriptActive : DialogueTrigger
{
    void Start()
    {
        TriggerDialogue();
        Debug.Log("TriggerDialogue");
    }

    protected override void AfterDialogue()
    {
        Debug.Log("After dialogue active");
    }
}