using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageFigureSpawner : MonoBehaviour
{
    int figureIndex;
    Figure generatedFigure;

    Transform figurePos;
    Transform danceButton;

    DefaultFigureBuilder defaultFigureBuilder;
    
    [SerializeField] GameObject ownFigureText;

    Vector3 figureLocalPosition = new Vector3(0f, 0f, 0f);
    Quaternion figureLocalRotation = new Quaternion(0f, -180f, 0f, 0f);

    Vector3 buttonActivePosition = new Vector3(0f, 0f, -1.0f);

    private void Awake()
    {        
        defaultFigureBuilder = new DefaultFigureBuilder();

        figurePos = transform.GetChild(0);
        danceButton = transform.GetChild(1);
    }

    public void GetFigureFromInventory(int index)
    {        
        figureIndex = index;

        if (Array.IndexOf(Managers.Game.collectedFigureIndex, figureIndex) == -1)
        {
            StartCoroutine(OwnFigureMessage());
        }
        else
        {
            if (generatedFigure != null)
            {
                Animator dance = generatedFigure.transform.GetComponent<Animator>(); 

                if(dance.GetBool("isDance"))
                {
                    dance.SetBool("isDance", false);
                    Managers.Sound.StopBGM(Define.BGM.Room);
                    Managers.Sound.PlayBGM(Define.BGM.Main);
                }

                Managers.Game.Destroy(generatedFigure);

                GenerateFigure(index);
                StartCoroutine(MovingUpDanceButton());
            }
            else
            {
                GenerateFigure(index);
                StartCoroutine(MovingUpDanceButton());
            }
        }
    }

    void GenerateFigure(int index)
    {
        generatedFigure = Managers.Game.Instantiate(index, figurePos);
        generatedFigure.gameObject.name = generatedFigure.name;
        generatedFigure.figureBuilder = defaultFigureBuilder;
        generatedFigure.figureBuilder.FigureOnEnable(generatedFigure, index);

        generatedFigure.myTransform.localPosition = figureLocalPosition;
        generatedFigure.myTransform.localRotation = figureLocalRotation;
        generatedFigure.myTransform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
    }

    IEnumerator OwnFigureMessage()
    {
        ownFigureText.SetActive(true);

        yield return Managers.Coroutine.WaitForSecondsEx(2f);

        ownFigureText.SetActive(false);
    }

    IEnumerator MovingUpDanceButton()
    {
        float elapsedTime = 0f;
        float waitTime = 3f;

        while (elapsedTime < waitTime)
        {
            danceButton.localPosition = Vector3.Lerp(danceButton.localPosition, buttonActivePosition, (elapsedTime / waitTime));

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        danceButton.localPosition = buttonActivePosition;
    }
}
