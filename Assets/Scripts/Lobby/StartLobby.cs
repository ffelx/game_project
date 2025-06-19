using Assets.Scripts.GlobalInformation;
using Assets.Scripts.Menu;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartLobby : MonoBehaviour
{
    [SerializeField] private DialogueTrigger _startDialogue;
    [SerializeField] private GameObject _moveToNextLevel;

    [SerializeField] private Text _moveToNextLevelText;
    [SerializeField] private float _typeSpeed = 0.05f;

    private void Awake()
    {
        _moveToNextLevel.SetActive(false);
    }

    void Start()
    {
        List<DialogueLine> lines = null;
        if (GlobalData.PreviousSceneName == "Level_1")
        {
            lines = new List<DialogueLine>()
            {
                new DialogueLine() {text = "Был пройден первый уровень."},
            };
        }
        else if (GlobalData.PreviousSceneName == "Level_2")
        {
            lines = new List<DialogueLine>()
            {
                new DialogueLine() {text = "Был пройден второй уровень."},
            };
        }
        else if (GlobalData.PreviousSceneName == "Level_3")
        {
            lines = new List<DialogueLine>()
            {
                new DialogueLine() {text = "Был пройден третий уровень."},
            };
        }
        else if (GlobalData.PreviousSceneName == "Level_4")
        {
            lines = new List<DialogueLine>()
            {
                new DialogueLine() { text = "Был пройден четвертый уровень."},
            };
        }
       
        if (lines != null)
        {
            var type = typeof(DialogueTrigger);
            FieldInfo field = type.GetField("dialogueLines", BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(_startDialogue, lines.ToArray());
            _startDialogue.TriggerDialogue();
            _startDialogue.onEndDialog.AddListener(ShowNextLevelMenu);

        }
    }

    public void HideNextLevelMenu()
    {
        _moveToNextLevel.SetActive(false);
    }

    public void ShowNextLevelMenu()
    {
        _moveToNextLevel.SetActive(true);
        string message = "Перейти на следующий уровень?";
        StartCoroutine(TypeTextInline(message, _moveToNextLevelText, _typeSpeed));
    }

    private IEnumerator TypeTextInline(string text, Text textComponent, float speed)
    {
        textComponent.text = "";
        foreach (char c in text)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(speed);
        }
    }

    public void MoveToNextLevel()
    {
        var save = SaveManager.Load(); 
        if (save.currentLevel == 2)
        {
            SceneManager.LoadScene("Level_2");
        }
        else if (save.currentLevel == 3)
        {
            SceneManager.LoadScene("Level_3");
        }
        else if (save.currentLevel == 4)
        {
            SceneManager.LoadScene("Level_4");
        }
        else if (save.currentLevel == 5)
        {
            SceneManager.LoadScene("Level_5");
        }
    }

    void Update()
    {
        
    }
}
