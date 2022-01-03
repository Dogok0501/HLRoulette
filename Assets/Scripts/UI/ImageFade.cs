using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFade : MonoBehaviour
{
    Image image;
    public bool isFadein;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        Color color = image.color;
        color.a = 0;
        image.color = color;

        if(isFadein)
            StartCoroutine(FadeIn(Define.FADE_IN_OUT_TIME));
        else
            StartCoroutine(FadeOut(Define.FADE_IN_OUT_TIME));
    }

    private IEnumerator FadeIn(float duration)
    {
        float elapsedTime = 0f;
        float percent = 0f;

        while (elapsedTime < duration)
        {
            percent = elapsedTime / duration;

            Color color = image.color;
            color.a = Mathf.Lerp(0, 1, percent);
            image.color = color;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        isFadein = false;
    }

    private IEnumerator FadeOut(float duration)
    {
        float elapsedTime = 0f;
        float percent = 0f;

        while (elapsedTime < duration)
        {
            percent = elapsedTime / duration;

            Color color = image.color;
            color.a = Mathf.Lerp(1, 0, percent);
            image.color = color;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        isFadein = true;
        gameObject.SetActive(false);
    }
}
