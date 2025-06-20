using Assets.Scripts.GlobalInformation;
using Assets.Scripts.Menu;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StartScenePrologue : MonoBehaviour
{
    [SerializeField] private GameObject _dialogueBoxPrefab;
    [SerializeField] private GameObject _splashScreenPrefab;

    [TextArea(3, 10)]
    [SerializeField] private string[] _lines;

    [SerializeField] private Sprite[] _backgrounds;

    [SerializeField] private float _splashDuration = 2f;  
    [SerializeField] private float _fadeDuration = 1f;

    [SerializeField] private Sprite _sprite1;
    [SerializeField] private Sprite _sprite2;
    [SerializeField] private Sprite _sprite3;


    private static int _countItems = 0;

    private void Start()
    {
        StartCoroutine(ShowSplashThenStart(_splashDuration, _fadeDuration));
    }

    private IEnumerator ShowSplashThenStart(float showTime, float fadeTime)
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        GameObject splash = Instantiate(_splashScreenPrefab, canvas.transform);
        RectTransform rectTransform = splash.GetComponent<RectTransform>();
        CanvasGroup canvasGroup = splash.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = splash.AddComponent<CanvasGroup>();
        }
     
        if (rectTransform != null)
        {
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
        }

        canvasGroup.alpha = 1f;

        yield return new WaitForSeconds(showTime);
        float elapsed = 0f;
        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeTime);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        Destroy(splash);
        //TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        DialogueLine[] dialogueLines = new DialogueLine[_lines.Length];

        for (int i = 0; i < _lines.Length; i++)
        {
            Sprite sprite;
            if (i < 3)
            {
                sprite = _sprite1;
            }
            else
            {
                sprite = _sprite2;
            }
            dialogueLines[i] = new DialogueLine
            {
                text = _lines[i],
                backgroundSprite = sprite
            };
        }

        GameObject dialogueObject = Instantiate(_dialogueBoxPrefab, transform);
        DialogueBox dialogue = dialogueObject.GetComponent<DialogueBox>();

        if (dialogue != null)
        {
            dialogue.StartDialogue(dialogueLines); 
        }
    }


    public static void AddItem()
    {
        _countItems += 1;
    }
    public static void TryEndLevel()
    {
        int maxItems = 3;
        if (_countItems >= maxItems)
        {
            GlobalData.PreviousSceneName = "Level_1";
            var data = SaveManager.Load();
            data.currentLevel = 2;  
            SaveManager.Save(data);
            SceneManager.LoadScene("Lobby");
        }
    }
}