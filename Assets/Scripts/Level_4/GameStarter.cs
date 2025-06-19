using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private GameObject backgroundToTurnOff;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private GameObject _nextDialog;

    [SerializeField] private GameObject _leftButton;
    [SerializeField] private GameObject _rightButton;

    private Image backgroundImage;

    void Start()
    {
        if (backgroundToTurnOff != null)
        {
            backgroundImage = backgroundToTurnOff.GetComponent<Image>();
            if (backgroundImage != null)
            {
                Color c = backgroundImage.color;
                c.a = 0f;
                backgroundImage.color = c;
            }
            backgroundToTurnOff.SetActive(false);
        }
        if (!Application.isMobilePlatform)
        {
            _leftButton.SetActive(false);
            _rightButton.SetActive(false);
        }
    }

    public void CloseGame()
    {
        if (backgroundToTurnOff != null)
        {
            backgroundToTurnOff.SetActive(true);
            _nextDialog.SetActive(true);
            if (backgroundImage != null)
            {
                StopAllCoroutines();
                StartCoroutine(FadeIn());
            }
        }
    }

    private IEnumerator FadeIn()
    {
        float elapsed = 0f;
        Color c = backgroundImage.color;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsed / fadeDuration);
            backgroundImage.color = c;
            yield return null;
        }
        c.a = 1f;
        backgroundImage.color = c;
    }
}
