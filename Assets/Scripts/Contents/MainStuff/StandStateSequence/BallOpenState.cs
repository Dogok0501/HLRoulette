using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOpenState : IStandSequence
{
    StandStates stand;

    public BallOpenState(StandStates stand)
    {
        this.stand = stand;
    }

    public void Init()
    {
        
    }

    public void UpdateAction()
    {
        PlayEffect();
        if (stand.transform.GetChild(6).CompareTag("Figure"))
        {
            ToFigureState();
        }
    }

    private void PlayEffect()
    {
        if(!stand.firework.isPlaying)
            stand.firework.Play();
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
        
    }

    public void ToFigureState()
    {
        stand.currentState = stand.figureState;
    }
}
