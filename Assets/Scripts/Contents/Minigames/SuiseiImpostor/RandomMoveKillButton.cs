using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomMoveKillButton : MonoBehaviour
{
    Vector2 originalPos;

    private void Awake()
    {
        originalPos = this.transform.position;
    }

    private void OnEnable()
    {
        this.transform.position = originalPos;
        StartCoroutine(RandomMove());
    }

    IEnumerator RandomMove()
    {
        while (this.gameObject.activeSelf)
        {
            float randX = Random.Range(550, 1510);
            float randY = Random.Range(250, 1190);
            this.transform.position = Vector2.Lerp(this.transform.position, new Vector2(randX, randY), 0.05f);
            //gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, lerpPos, (elapsedTime / duration));
            yield return null;//Managers.Coroutine.WaitForSecondsEx(0.8f);
        }
    }
}
