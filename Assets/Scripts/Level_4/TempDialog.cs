using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Assets.Scripts.Level_4
{
    internal class TempDialog : Dialog_1_1
    {
        [SerializeField] private float autoHideAfterSeconds = 1.1f;

        private Coroutine hideCoroutine;
        private bool hasSpawned = false;

        private void OnEnable()
        {
            hasSpawned = false; 
            DestroyAllDialogs(); 
            TriggerDialogue();  
            if (hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine);

            }
            hideCoroutine = StartCoroutine(AutoHide());
        }

        private IEnumerator AutoHide()
        {
            yield return new WaitForSeconds(autoHideAfterSeconds);
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            DestroyAllDialogs();
            hasSpawned = false;
        }

        public override void TriggerDialogue()
        {
            if (hasSpawned) return; 
            hasSpawned = true;

            DestroyAllDialogs();

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
            dialogueObject.transform.SetAsLastSibling();

            RectTransform rt = dialogueObject.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchoredPosition = Vector2.zero;
                rt.localScale = Vector3.one;
            }

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

        private void DestroyAllDialogs()
        {
            DialogueBox[] allDialogs = FindObjectsOfType<DialogueBox>(true);
            foreach (var dlg in allDialogs)
            {
                if (dlg != null && dlg.gameObject != null)
                {
                    Destroy(dlg.gameObject);

                }
            }
        }
    }

}
