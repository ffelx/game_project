using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreenIntro : MonoBehaviour
{
    [SerializeField] private GameObject _blackScreenPrefab;
    [SerializeField, TextArea(2, 10)] private string _displayText = "";
    [SerializeField] private GameObject _objectToActivateAfter; 
    [SerializeField] private float _showDuration = 2f;
    [SerializeField] private float _fadeDuration = 1f;

    private void Start()
    {
        StartCoroutine(ShowBlackScreenThenActivate());
    }

    private IEnumerator ShowBlackScreenThenActivate()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        
        GameObject blackScreen = Instantiate(_blackScreenPrefab, canvas.transform);
        RectTransform rect = blackScreen.GetComponent<RectTransform>();
        CanvasGroup canvasGroup = blackScreen.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = blackScreen.AddComponent<CanvasGroup>();
        }

        Text textComponent = blackScreen.GetComponentInChildren<Text>();
        if (textComponent != null)
        {
            textComponent.text = _displayText;
        }
        else
        {
            Debug.LogWarning("Нет элемента текста");
        }

        if (rect != null)
        {
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
        }

        canvasGroup.alpha = 1f;

        yield return new WaitForSeconds(_showDuration);

        float elapsed = 0f;
        while (elapsed < _fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / _fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        Destroy(blackScreen);

        if (_objectToActivateAfter != null)
        {
            _objectToActivateAfter.SetActive(true);
        }
    }
}
