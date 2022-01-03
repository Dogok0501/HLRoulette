using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarEffect : MonoBehaviour
{
    Poolable starMainBody;
    Image starImage;
    Vector2 starDirction;
    float starSize;

    [SerializeField] Color[] starColors;

    void OnEnable()
    {
        Init();
        StarEffectLoop();
    }

    private void Init()
    {
        starMainBody = GetComponent<Poolable>();
        starImage = GetComponent<Image>();
        StarRandomColor();
        StarTransform();
    }

    private void StarRandomColor()
    {
        starImage.color = starColors[Random.Range(0, starColors.Length)];
    }

    private void StarTransform()
    {
        starDirction = new Vector2(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));
        starSize = Random.Range(Define.STAR_MIN_SIZE, Define.STAR_MAX_SIZE);
        transform.localScale = new Vector3(starSize, starSize);
    }

    private void StarEffectLoop()
    {
        StartCoroutine(StarEffectLife());
    }

    IEnumerator StarEffectLife()
    {
        float elapsedTime = 0f;
        float waitTime = 2f;

        while (elapsedTime < waitTime)
        {
            transform.Translate(starDirction * Define.STAR_MOVE_SPEED);
            transform.localScale = Vector2.Lerp(transform.localScale, Vector2.zero, (elapsedTime / waitTime));

            Color color = starImage.color;
            color.a = Mathf.Lerp(starImage.color.a, 0, (elapsedTime / waitTime));
            starImage.color = color;

            elapsedTime += Time.deltaTime;

            if (starImage.color.a < 0.01f)
            {
                EffectReset();
                Managers.Game.Destroy(starMainBody);
            }                

            yield return null;
        }
    }

    private void EffectReset()
    {
        starDirction = new Vector2(0.0f, 0.0f);
        Color color = starImage.color;
        color.a = 1.0f;
        starImage.color = color;
    }
}
