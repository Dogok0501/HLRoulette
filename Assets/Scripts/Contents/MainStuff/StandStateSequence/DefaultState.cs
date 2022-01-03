using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : IStandSequence
{
    StandStates stand;

    public DefaultState(StandStates stand)
    {
        this.stand = stand;
    }

    public void Init()
    {

    }

    public void UpdateAction()
    {        
        if (Managers.Game.isGachaing && stand.CountChildren() == 7 && stand.transform.GetChild(6).CompareTag("Ball"))
            ToBallSpawnState();
    }
    

    public void ToBallOpenState()
    {

    }

    public void ToBallSpawnState()
    {
        stand.currentState = stand.ballSpawnState;
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
