using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureSellState : IStandSequence
{
    StandStates stand;

    public FigureSellState(StandStates stand)
    {
        this.stand = stand;
    }

    public void Init()
    {

    }

    public void UpdateAction()
    {
        if (!Managers.Game.isGachaing)
        {
            HideFigureInfo();
            ResetParticleEffect();
            ToDefaultState();
        }
    }

    private void ResetParticleEffect()
    {
        stand.firework.Stop();
        stand.circle.Stop();
    }

    private void HideFigureInfo()
    {
        for (int i = 0; i < stand.RarityStars.transform.childCount; i++)
        {
            stand.RarityStars.transform.GetChild(i).gameObject.SetActive(false);
        }
        stand.figureNameCanvas.gameObject.SetActive(false);
    }

    public void ToBallOpenState()
    {

    }

    public void ToBallSpawnState()
    {

    }

    public void ToDefaultState()
    {
        stand.currentState = stand.defaultState;
    }

    public void ToFigureSellState()
    {

    }
    
    public void ToFigureState()
    {

    }
}
