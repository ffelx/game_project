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
            Debug.Log("�����");
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
        "���������� ����� ������������ �� �����������, ���������� �� �������� ��������� ����������� �������. " +
        "��� ����� ������� ���� ��������, ��������� ������� ������ � �����. ������ � ������ ����. " +
        "����� �������, ���� ����������, ���� ���������.",
        
        "������ ���������� ������� ������, �� �� ������ ������ �� ������ ��������. " +
        "�� �� �����, ������ ������� � ����� ���������, ����� �� �����, � ��� ���������. " +
        "��-��������� ���� ��������: \"����� ������ � �������� ���������� ���������� ����� �����������. " +
        "��������� � �������������� ���������.\"",
    };

}
