using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    [SerializeField] private Text _textComponent;
    [SerializeField] private Image _windowImage;
    [SerializeField] private float _fadeDuration = 0.5f;
    [SerializeField] private float _typeSpeed = 0.05f;

    private int _currentLine = 0;
    

    private bool isTyping = false;
    void Start()
    {
        //HideWindow();
        ShowNextLine();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                _textComponent.text = lines[_currentLine - 1];
                isTyping = false;
            }
            else
            {
                ShowNextLine();
            }
        }
    }

    void ShowNextLine()
    {
        if (lines == null || _currentLine >= lines.Length)
        {
            HideWindow();
            Destroy(this);
            Debug.Log("Конец");
            return;
        }

        StartCoroutine(TypeText(lines[_currentLine]));
        _currentLine++;
    }

    IEnumerator TypeText(string message)
    {
        isTyping = true;
        _textComponent.text = "";

        foreach (char c in message)
        {
            _textComponent.text += c;
            yield return new WaitForSeconds(_typeSpeed);
        }

        isTyping = false;
    }

    void HideWindow()
    {
        _windowImage.CrossFadeAlpha(0f, _fadeDuration, false);
        _textComponent.CrossFadeAlpha(0f, _fadeDuration, false);
    }

    private string[] lines = {
        "Экокорабль «Гея» приземляется на координатах, полученных из частично уцелевших спутниковых архивов. " +
        "Это место некогда было деревней, окружённой густыми лесами и рекой. Сейчас — мёртвая зона. " +
        "Почва выжжена, вода загрязнена, дома разрушены.",
        
        "Флорен испытывает чувство дежавю, но не помнит ничего из своего прошлого. " +
        "Он не знает, почему планета в таком состоянии, зачем он здесь, и что произошло. " +
        "ИИ-ассистент лишь сообщает: \"Поиск данных о причинах деградации окружающей среды активирован. " +
        "Приоритет — восстановление контекста.\"",
    };

}
