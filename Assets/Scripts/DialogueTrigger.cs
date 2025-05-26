using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBoxPrefab;
    [SerializeField] private DialogueLine[] dialogueLines;
    [SerializeField] private Image background; 

    public void TriggerDialogue()
    {
        if (dialogueBoxPrefab == null)
        {
            Debug.LogError("DialogueBox prefab not assigned!", this);
            return;
        }

        GameObject dialogueObject = Instantiate(dialogueBoxPrefab, transform.position, Quaternion.identity, transform);
        DialogueBox dialogue = dialogueObject.GetComponent<DialogueBox>();

        if (dialogue == null)
        {
            Debug.LogError("DialogueBox component missing on prefab!", dialogueObject);
            Destroy(dialogueObject);
            return;
        }

        if (dialogueLines == null || dialogueLines.Length == 0)
        {
            Debug.LogWarning("No dialogue lines assigned!", this);
            Destroy(dialogueObject);
            return;
        }

        dialogue.StartDialogue(dialogueLines, background, AfterDialogue);
    }

    protected virtual void AfterDialogue()
    {
        Debug.Log("After dialogue");
    }

    void OnMouseDown()
    {
        TriggerDialogue();
    }
}