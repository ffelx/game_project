using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] protected GameObject dialogueBoxPrefab;
    [SerializeField] protected DialogueLine[] dialogueLines;
    [SerializeField] protected Image background;

    public UnityEvent onEndDialog;

    public virtual void TriggerDialogue()
    {
        if (dialogueBoxPrefab == null)
        {
            Debug.LogError("DialogueBox prefab not assigned!", this);
            return;
        }

        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("No Canvas found in the scene!", this);
            return;
        }

        GameObject dialogueObject = Instantiate(dialogueBoxPrefab, canvas.transform);

        if (canvas.sortingOrder >= 32000)
        {
            dialogueObject.transform.SetAsFirstSibling();
        }
        else
        {
            dialogueObject.transform.SetAsLastSibling();
        }

        RectTransform rt = dialogueObject.GetComponent<RectTransform>();
        if (rt != null)
        {
            rt.anchoredPosition = Vector2.zero;
            rt.localScale = Vector3.one;
        }

        DialogueBox dialogue = dialogueObject.GetComponent<DialogueBox>();
        if (dialogue == null)
        {
            Destroy(dialogueObject);
            return;
        }

        if (dialogueLines == null || dialogueLines.Length == 0)
        {
            Destroy(dialogueObject);
            return;
        }

        dialogue.StartDialogue(dialogueLines, background, AfterDialogue);
        //if (dialogueBoxPrefab == null)
        //{
        //    Debug.LogError("DialogueBox prefab not assigned!", this);
        //    return;
        //}

        //Canvas canvas = FindObjectOfType<Canvas>();
        //if (canvas == null)
        //{
        //    Debug.LogError("No Canvas found in the scene!", this);
        //    return;
        //}

        //GameObject dialogueObject = Instantiate(dialogueBoxPrefab, canvas.transform);

        //dialogueObject.transform.SetAsLastSibling();

        //RectTransform rt = dialogueObject.GetComponent<RectTransform>();
        //if (rt != null)
        //{
        //    rt.anchoredPosition = Vector2.zero;
        //    rt.localScale = Vector3.one;
        //}

        //Canvas dialogueCanvas = dialogueObject.GetComponent<Canvas>();
        //if (dialogueCanvas == null)
        //{
        //    dialogueCanvas = dialogueObject.AddComponent<Canvas>();
        //    dialogueObject.AddComponent<UnityEngine.UI.GraphicRaycaster>();
        //}

        //if (!dialogueCanvas.overrideSorting)
        //{
        //    dialogueCanvas.overrideSorting = true;
        //    dialogueCanvas.sortingOrder = 100;
        //}

        //DialogueBox dialogue = dialogueObject.GetComponent<DialogueBox>();
        //if (dialogue == null)
        //{
        //    Destroy(dialogueObject);
        //    return;
        //}

        //if (dialogueLines == null || dialogueLines.Length == 0)
        //{
        //    Destroy(dialogueObject);
        //    return;
        //}

        //dialogue.StartDialogue(dialogueLines, background, AfterDialogue);
        //if (dialogueBoxPrefab == null)
        //{
        //    Debug.LogError("DialogueBox prefab not assigned!", this);
        //    return;
        //}

        //Canvas canvas = FindObjectOfType<Canvas>();
        //if (canvas == null)
        //{
        //    Debug.LogError("No Canvas found in the scene!", this);
        //    return;
        //}

        //GameObject dialogueObject = Instantiate(dialogueBoxPrefab, canvas.transform);
        //dialogueObject.transform.SetAsLastSibling(); 

        //RectTransform rt = dialogueObject.GetComponent<RectTransform>();
        //if (rt != null)
        //{
        //    rt.anchoredPosition = Vector2.zero;
        //    rt.localScale = Vector3.one;
        //}

        //DialogueBox dialogue = dialogueObject.GetComponent<DialogueBox>();
        //if (dialogue == null)
        //{
        //    Destroy(dialogueObject);
        //    return;
        //}

        //if (dialogueLines == null || dialogueLines.Length == 0)
        //{
        //    Destroy(dialogueObject);
        //    return;
        //}

        //dialogue.StartDialogue(dialogueLines, background, AfterDialogue);
    }

    protected virtual void AfterDialogue()
    {
        onEndDialog?.Invoke();
        Debug.Log("After dialogue");
    }

    void OnMouseDown()
    {
        TriggerDialogue();
    }
}