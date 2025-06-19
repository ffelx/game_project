using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class End : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 2f;
    [SerializeField] private string targetScene = "MainMenu";

    private Image img;

    void Start()
    {
        img = GetComponent<Image>();
        StartCoroutine(FadeOutAndLoad());
    }

    IEnumerator FadeOutAndLoad()
    {
        float time = 0f;
        Color startColor = img.color;
        startColor.a = 0f;
        img.color = startColor;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Clamp01(time / fadeDuration);
            img.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }
        SceneManager.LoadScene(targetScene);
        yield return new WaitForSeconds(1); 

       
        Destroy(gameObject);
    }
}
