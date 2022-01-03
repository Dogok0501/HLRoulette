using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnState : IStandSequence
{
    StandStates stand;

    public BallSpawnState(StandStates stand)
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
            ToBallOpenState();
        }
    }

    private void PlayEffect()
    {
        if(!stand.circle.isPlaying)
            stand.circle.Play();
    }

    public void ToBallOpenState()
    {
        stand.currentState = stand.ballOpenState;
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

    }
}
