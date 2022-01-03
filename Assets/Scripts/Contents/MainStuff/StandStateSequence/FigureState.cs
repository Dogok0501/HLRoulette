using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FigureState : IStandSequence
{
    StandStates stand;
    Figure figure;

    string figureName;
    string figureRarity;

    TextMeshPro figureNameTM;

    bool onetime = false;

    public FigureState(StandStates stand)
    {
        this.stand = stand;
    }

    public void Init()
    {
        
    }

    public void UpdateAction()
    {
        if(!onetime)
        {
            GetFigureInfo(ref figureName, ref figureRarity);
            onetime = true;
        }

        DisplayFigureInfo();
        GetTouchPosition();

        if (Managers.Game.isDisplayFigureInfo == false)
        {
            onetime = false;
            ToFigureSellState();
        }
    }

    private void GetTouchPosition()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        if (Input.GetMouseButton(0))
        {
            Vector2 touchPosition = Input.mousePosition;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.CompareTag("Figure"))
                {
                    Managers.Game.isDisplayFigureInfo = false;
                }
            }
        }
#else        
        if(Input.touchCount > 0)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.CompareTag("Figure"))
                {
                    Managers.Game.isDisplayFigureInfo = false;
                }
            }
        }        
#endif
    }

    private void GetFigureInfo(ref string figureName, ref string figureRarity)
    {
        figure = stand.transform.GetChild(6).GetComponent<Figure>();

        figureName = figure.figureItem.name;
        figureRarity = figure.figureItem.rarity;
    }

    private void DisplayFigureInfo()
    {        
        //피규어 레어도
        Managers.Instance.StartCoroutine(DisplayRarity());

        //피규어 이름
        stand.figureNameCanvas.gameObject.SetActive(true);
        stand.figureNameTM.text = figureName;
    }       

    private IEnumerator DisplayRarity()
    {
        switch (figureRarity)
        {
            case "Normal":
                for (int i = 0; i < Define.NORMAL_STAR; i++)
                {
                    GameObject rarityStar = stand.RarityStars.transform.GetChild(i).gameObject;
                    rarityStar.SetActive(true);          
                    yield return Managers.Coroutine.WaitForSecondsEx(0.2f);
                }
                break;

            case "Rare":
                for (int i = 0; i < Define.RARE_STAR; i++)
                {
                    GameObject rarityStar = stand.RarityStars.transform.GetChild(i).gameObject;
                    rarityStar.SetActive(true);
                    yield return Managers.Coroutine.WaitForSecondsEx(0.2f);
                }
                break;

            case "Ultra":
                for (int i = 0; i < Define.ULTRA_STAR; i++)
                {
                    GameObject rarityStar = stand.RarityStars.transform.GetChild(i).gameObject;
                    rarityStar.SetActive(true);
                    yield return Managers.Coroutine.WaitForSecondsEx(0.2f);
                }
                break;
        }
    }        

    public void ToBallOpenState()
    {

    }

    public void ToBallSpawnState()
    {

    }

    public void ToDefaultState()
    {

    }

    public void ToFigureSellState()
    {
        stand.currentState = stand.figureSellState;
    }

    public void ToFigureState()
    {

    }    
}
