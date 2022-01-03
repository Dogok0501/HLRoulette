using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnFigure : MonoBehaviour
{
    GameObject ball;
    Figure generatedFigure;
    DefaultFigureBuilder defaultFigureBuilder;

    BallEffect ballEffect;

    Vector3 figureLocalPosition = new Vector3(0, 1f, 0);

    int figureIndex;
    public int GetFigureIndex() { { return figureIndex; } }

    private void OnEnable()
    {
        Init();
        TouchUpdate();
        RandomFigureGenerate();
    }

    private void Init()
    {
        ball = transform.gameObject;
        defaultFigureBuilder = new DefaultFigureBuilder();
        ballEffect = GetComponent<BallEffect>();
    }

    private void TouchUpdate()
    {
        StartCoroutine(GetTouchPosition());
    }

    private IEnumerator GetTouchPosition()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        while (!Managers.Game.isGameOver)
        {
            if (Input.GetMouseButton(0))
            {         
                Vector2 touchPosition = Input.mousePosition;

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(touchPosition);                

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.transform.gameObject == ball)
                    {
                        StartCoroutine(ballEffect.SpinBall());
                        yield return Managers.Coroutine.WaitForSecondsEx(2f);
                        SpawnGeneratedFigure();
                        StartCoroutine(SpinFigure());

                        ball.GetComponent<MeshRenderer>().enabled = false;

                        yield return Managers.Coroutine.WaitForSecondsEx(1f);
                        PlayFanfare();

                        ball.GetComponent<MeshRenderer>().enabled = true;
                        Managers.Game.Destroy(ball.GetComponent<Poolable>());
                    }
                }
            }
            yield return null;
        }
#else        
        while(!Managers.Game.isGameOver)
        {
            if(Input.touchCount > 0)
            {
                Vector2 touchPosition = Input.GetTouch(0).position;

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(touchPosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.transform.gameObject == ball)
                    {
                        StartCoroutine(ballEffect.SpinBall());
                        yield return Managers.Coroutine.WaitForSecondsEx(2f);
                        SpawnGeneratedFigure();
                        StartCoroutine(SpinFigure());

                        ball.GetComponent<MeshRenderer>().enabled = false;

                        yield return Managers.Coroutine.WaitForSecondsEx(1f);
                        PlayFanfare();

                        ball.GetComponent<MeshRenderer>().enabled = true;
                        Managers.Game.Destroy(ball.GetComponent<Poolable>());
                    }
                }    
            }                     
            yield return null;
        }
#endif
    }

    private void RandomFigureGenerate()
    {
        int rarity = UnityEngine.Random.Range(1, 100);

        if (rarity <= Define.NORMAL_CHANCE) // 1~76
        {
            figureIndex = UnityEngine.Random.Range(1008, 1015);
        }
        else if (rarity > Define.NORMAL_CHANCE && rarity <= 100 - Define.ULTRA_CHANCE) // 77 ~ 96
        {
            figureIndex = UnityEngine.Random.Range(1002, 1009);
        }
        else if (rarity > 100 - Define.ULTRA_CHANCE) // 97 ~ 100
        {
            figureIndex = UnityEngine.Random.Range(1000, 1002);
        }
    }    

    private void SpawnGeneratedFigure()
    {      
        generatedFigure = Managers.Game.Instantiate(figureIndex, ball.transform.parent);        
        generatedFigure.figureBuilder = defaultFigureBuilder;
        generatedFigure.figureBuilder.FigureOnEnable(generatedFigure, figureIndex);

        if (generatedFigure.figureItem.rarity != "Normal")
            StartCoroutine(generatedFigure.GetComponent<DissolveHelper>().Appearing(0.0060f));

        generatedFigure.myTransform.localPosition = figureLocalPosition;
    }

    private IEnumerator SpinFigure()
    {
        float elapsedTime = 0f;
        float duration = 1f;

        Quaternion originalRotation = generatedFigure.myTransform.rotation;
        Vector3 originalScale = generatedFigure.myTransform.localScale;

        while (elapsedTime < duration)
        {
            Vector3 targetScale = new Vector3(0.0f, 0.0f, 0.0f);

            generatedFigure.myTransform.Rotate(0, 60, 0);
            generatedFigure.myTransform.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime);
            elapsedTime += Time.deltaTime;

            if (elapsedTime > 0.95f)
                ResetFigure(originalScale, originalRotation);

            yield return null;
        }
    }

    private void PlayFanfare()
    {
        switch (generatedFigure.figureItem.rarity)
        {
            case "Normal":
                Managers.Sound.PlaySFX(Define.SFX.NormalPop);
                break;

            case "Rare":
                Managers.Sound.PlaySFX(Define.SFX.RarePop);
                break;

            case "Ultra":
                Managers.Sound.PlaySFX(Define.SFX.UltraPop);
                break;
        }
    }

    private void ResetFigure(Vector3 originalScale, Quaternion originalRotation)
    {
        generatedFigure.myTransform.localScale = originalScale;
        generatedFigure.myTransform.rotation = originalRotation;
    }
}
