using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStandSequence
{
    void Init();

    void UpdateAction();

    void ToDefaultState();

    void ToBallSpawnState();

    void ToBallOpenState();

    void ToFigureState();

    void ToFigureSellState();
}
