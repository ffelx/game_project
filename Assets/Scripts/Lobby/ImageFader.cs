using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFader : MonoBehaviour
{
    [SerializeField] private Image image1;
    [SerializeField] private Image image2;

    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float holdDuration = 3f;

    private CanvasGroup group1;
    private CanvasGroup group2;

    void Awake()
    {
        group1 = image1.gameObject.AddComponent<CanvasGroup>();
        group2 = image2.gameObject.AddComponent<CanvasGroup>();

        SetAlpha(group1, 1f);
        SetAlpha(group2, 0f);

        StartCoroutine(SwitchImages());
    }

    System.Collections.IEnumerator SwitchImages()
    {
        while (true)
        {
            yield return FadeBetween(group1, group2, fadeDuration);
            yield return new WaitForSeconds(holdDuration);
            yield return FadeBetween(group2, group1, fadeDuration);
            yield return new WaitForSeconds(holdDuration);
        }
    }

    System.Collections.IEnumerator FadeBetween(CanvasGroup from, CanvasGroup to, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            SetAlpha(from, 1f - t);
            SetAlpha(to, t);

            yield return null;
        }

        SetAlpha(from, 0f);
        SetAlpha(to, 1f);
    }

    void SetAlpha(CanvasGroup group, float alpha)
    {
        group.alpha = alpha;
    }
}
