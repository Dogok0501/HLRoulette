using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public int moneyHold;
    public int gachaStack;
    public int[] collectedFigureIndex = new int[Define.TOTAL_FIGURE_NUM];

    public bool isGachaing;
    public bool isGameOver;
    public bool isPull;
    public bool isDisplayFigureInfo;

    public bool isMinigameOver;

    public void Init()
    {       
        moneyHold = Managers.SaveLoad.moneyHold;
        gachaStack = Managers.SaveLoad.gachaStack;
        collectedFigureIndex = Managers.SaveLoad.collectedFigure;

        isGachaing = false;
        isGameOver = false;
        isPull = false;

        isDisplayFigureInfo = false;

        isMinigameOver = false;
    }

    #region figure instantiate and destroy
    public Figure Instantiate(int index, Transform parent = null)
    {       
        return Managers.Pool.Pop(index, parent);
    }
    
    public void Destroy(Figure figure)
    {
        if (figure == null)
            return;
        Managers.Pool.Push(figure);
    }
    #endregion

    #region common instantiate and destroy
    public Poolable Instantiate(GameObject gameObject, Transform parent = null)
    {
        return Managers.Pool.Pop(gameObject, parent);
    }

    public void Destroy(Poolable poolable)
    {
        if (poolable == null)
            return;
        Managers.Pool.Push(poolable);
    }
    #endregion
}
