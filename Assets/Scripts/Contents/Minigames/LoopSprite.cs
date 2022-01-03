using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopSprite : MonoBehaviour
{
    [SerializeReference] Sprite[] sprites;
    Image image;
    int counter;
    [SerializeReference] float delay = 0.1f;

    private void Awake()
    {        
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        counter = 0;
        StartCoroutine(Looping(delay));
    }

    private void OnDisable()
    {
        StopCoroutine(Looping(delay));
    }

    IEnumerator Looping(float delay)
    {
        while(true)
        {
            image.sprite = sprites[counter];

            if (counter < sprites.Length - 1)
                counter++;
            else
                counter = 0;

            yield return Managers.Coroutine.WaitForSecondsEx(delay);
        }
    }
}
