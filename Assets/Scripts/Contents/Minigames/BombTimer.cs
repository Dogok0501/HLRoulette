using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombTimer : MonoBehaviour
{
    [SerializeReference] Sprite[] sprites;
    Image image;
    int counter;

    [SerializeReference] Animator potPanel, pot;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        counter = 0;
        image.sprite = sprites[counter];
        StartCoroutine(ExplosionTimer());
    }

    IEnumerator ExplosionTimer()
    {
        yield return Managers.Coroutine.WaitForSecondsEx(2.0f);

        float elapsedTime = 0f;
        float waitTime = Define.MINIGAME_TIMER;

        while (elapsedTime < waitTime)
        {
            image.sprite = sprites[counter];
            if (counter < sprites.Length - 1)
               counter++;

            elapsedTime += Time.deltaTime;

            yield return Managers.Coroutine.WaitForSecondsEx((waitTime / sprites.Length));

            if (image.sprite == sprites[sprites.Length - 1])
            {
                potPanel.SetBool("Zoom", false);
                pot.SetBool("is Game End", true);

                yield return Managers.Coroutine.WaitForSecondsEx(0.1f);

                this.transform.parent.gameObject.SetActive(false);
            }                
        }
    }
}
