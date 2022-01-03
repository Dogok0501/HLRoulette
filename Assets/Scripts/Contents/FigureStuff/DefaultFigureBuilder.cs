using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultFigureBuilder : IFigureBuilder
{
    public void FigureOnEnable(Figure figure, int index)
    {
        figure.figureItem = Managers.Data.figureDataDict[index];        
        figure.myGameObject.name = Managers.Data.figureDataDict[index].name;
    }
}
