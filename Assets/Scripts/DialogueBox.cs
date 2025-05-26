using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class DialogueLine
{
    [TextArea(3, 10)]
    public string text;

    public Sprite backgroundSprite;
}

public class DialogueBox : MonoBehaviour
{
    private Image _targetBackground; 
    [SerializeField] private Transform _backgroundContainer; 
    [SerializeField] private Text _textComponent;
    [SerializeField] private Image _windowImage;
    [SerializeField] private float _fadeDuration = 0.5f;
    [SerializeField] private float _typeSpeed = 0.05f;

    [SerializeField, HideInInspector]
    private string[] _oldLines;

    private DialogueLine[] _lines;
    private int _currentLine = 0;
    private bool _isTyping = false;
    private GameObject _currentBackgroundInstance;
    private Action _onDialogueEnd;

    public void StartDialogue(DialogueLine[] newLines, Image targetBackground, Action onDialogueEnd = null)
    {
        _lines = newLines;
        _targetBackground = targetBackground;
        _currentLine = 0;
        _onDialogueEnd = onDialogueEnd;
        ShowNextLine();
    }

    public void StartDialogue(DialogueLine[] newLines, Action onDialogueEnd = null)
    {
        _lines = newLines;
        _currentLine = 0;
        _onDialogueEnd = onDialogueEnd;
        ShowNextLine();
    }

    private void ShowNextLine()
    {
        if (_lines == null || _currentLine >= _lines.Length)
        {
            _onDialogueEnd?.Invoke();
            HideWindow();
            Destroy(gameObject);
            return;
        }

        DialogueLine currentLine = _lines[_currentLine];

        if (currentLine.backgroundSprite != null && _targetBackground != null)
        {
            _targetBackground.sprite = currentLine.backgroundSprite;
        }

        if (string.IsNullOrEmpty(currentLine.text))
        {
            _windowImage.enabled = false;
            _textComponent.text = "";
        }
        else
        {
            _windowImage.enabled = true;
            StartCoroutine(TypeText(currentLine.text));
        }

        _currentLine += 1;
    }

    private IEnumerator TypeText(string message)
    {
        _isTyping = true;
        _textComponent.text = "";

        foreach (char c in message)
        {
            _textComponent.text += c;
            yield return new WaitForSeconds(_typeSpeed);
        }

        _isTyping = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_isTyping)
            {
                StopAllCoroutines();
                _textComponent.text = _lines[_currentLine - 1].text;
                _isTyping = false;
            }
            else
            {
                ShowNextLine();
            }
        }
    }

    private void HideWindow()
    {
        _windowImage.CrossFadeAlpha(0f, _fadeDuration, false);
        _textComponent.CrossFadeAlpha(0f, _fadeDuration, false);
    }
}