using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
//using UnityEditor.VersionControl;

namespace Assets.Scripts.Level_2
{
    internal class Shower
    {
        public static CanvasGroup resultCanvas;
        public static Text resultText;

        public static System.Collections.IEnumerator FadeResultRoutine1(string message, bool victory)
        {
            if (resultCanvas == null) Shower.CreateResultUI();
            resultText.text = message;
            resultText.color = victory ? new Color(0.8f, 1f, 0.8f) : new Color(1f, 0.7f, 0.7f);

            float duration = 0.5f;
            float holdTime = 3f;

            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                float a = t / duration;
                resultCanvas.alpha = a;
                yield return null;
            }
            resultCanvas.alpha = 1;

            yield return new WaitForSeconds(holdTime);

            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                float a = 1 - (t / duration);
                resultCanvas.alpha = a;
                yield return null;
            }
            resultCanvas.alpha = 0;
        }

        public static System.Collections.IEnumerator FadeResultRoutine(string message, bool victory)
        {
            if (resultCanvas == null) Shower.CreateResultUI();

            resultCanvas.gameObject.SetActive(true); // <<< Добавлено

            resultText.text = message;
            resultText.color = victory ? new Color(0.8f, 1f, 0.8f) : new Color(1f, 0.7f, 0.7f);

            float duration = 0.5f;
            float holdTime = 3f;

            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                float a = t / duration;
                resultCanvas.alpha = a;
                yield return null;
            }
            resultCanvas.alpha = 1;

            yield return new WaitForSeconds(holdTime);

            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                float a = 1 - (t / duration);
                resultCanvas.alpha = a;
                yield return null;
            }
            resultCanvas.alpha = 0;


            resultCanvas.gameObject.SetActive(false); 
        }

        public static void CreateResultUI()
        {
            GameObject canvasGO = new GameObject("ResultCanvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            Canvas canvas = canvasGO.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 1000;

            CanvasScaler scaler = canvasGO.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);

            GameObject panelGO = new GameObject("Panel", typeof(Image), typeof(CanvasGroup));
            panelGO.transform.SetParent(canvasGO.transform, false);
            Image panelImage = panelGO.GetComponent<Image>();
            panelImage.color = new Color(0f, 0f, 0f, 0.75f);

            RectTransform panelRect = panelGO.GetComponent<RectTransform>();
            panelRect.anchorMin = Vector2.zero;
            panelRect.anchorMax = Vector2.one;
            panelRect.offsetMin = Vector2.zero;
            panelRect.offsetMax = Vector2.zero;

            resultCanvas = panelGO.GetComponent<CanvasGroup>();
            resultCanvas.alpha = 0;

            GameObject textGO = new GameObject("Text", typeof(Text));
            textGO.transform.SetParent(panelGO.transform, false);
            resultText = textGO.GetComponent<Text>();
            resultText.alignment = TextAnchor.MiddleCenter;
            resultText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            resultText.fontSize = 64;
            resultText.horizontalOverflow = HorizontalWrapMode.Wrap;
            resultText.verticalOverflow = VerticalWrapMode.Overflow;
            resultText.rectTransform.anchorMin = Vector2.zero;
            resultText.rectTransform.anchorMax = Vector2.one;
            resultText.rectTransform.offsetMin = Vector2.zero;
            resultText.rectTransform.offsetMax = Vector2.zero;
        }
    }
}
