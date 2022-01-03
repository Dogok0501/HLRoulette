using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEffect : MonoBehaviour
{
    Vector3 originalPos;
    Vector3 originalScale;
    Quaternion originalRotation;
    Material mat;

    public void OnEnable()
    {
        mat = GetComponent<Renderer>().material;
        mat.SetFloat("_SplitValue", 1);
    }

    public IEnumerator SpinBall()
    {        
        float elapsedTime = 0f;
        float duration = 2f;        

        originalPos = transform.localPosition;
        originalScale = transform.localScale;
        originalRotation = transform.rotation;

        Managers.Sound.PlaySFX(Define.SFX.BallSpin);

        while (elapsedTime < duration)
        {
            Vector3 randScale = new Vector3(Random.Range(2f, 5f), Random.Range(2f, 5f), Random.Range(2f, 5f));
            float x = Random.Range(-0.5f, 0.5f) * 0.15f;
            float y = Random.Range(-0.5f, 0.5f) * 0.15f;

            transform.Rotate(30, 60, 30);
            transform.localScale = Vector3.Lerp(transform.localScale, randScale, elapsedTime);
            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            mat.SetFloat("_SplitValue", 1 - (elapsedTime / duration));
            elapsedTime += Time.deltaTime;

            if (elapsedTime > 1.9)
            {
                ResetBall(originalPos, originalScale, originalRotation);
                Managers.Sound.StopSFX(Define.SFX.BallSpin);
            }                

            yield return null;
        }
    }

    private void ResetBall(Vector3 originalPos, Vector3 originalScale, Quaternion originalRotation)
    {
        transform.localPosition = originalPos;
        transform.localScale = originalScale;
        transform.rotation = originalRotation;
    }
}