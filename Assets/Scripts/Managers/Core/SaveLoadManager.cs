using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager
{
    public string moneyHold_key = "MoneyHold";
    public int moneyHold = 100;

    public string gachaStack_key = "GachaStack";
    public int gachaStack = 0;

    public string collectedFigure_key = "CollectedFigure";
    public int[] collectedFigure = new int[Define.TOTAL_FIGURE_NUM];

    public void Init()
    {
        if (PlayerPrefs.HasKey(moneyHold_key))
            moneyHold = PlayerPrefs.GetInt(moneyHold_key);

        if (PlayerPrefs.HasKey(gachaStack_key))
            gachaStack = PlayerPrefs.GetInt(gachaStack_key);

        collectedFigure = PlayerPrefsX.GetIntArray(collectedFigure_key);        
    }

    public void UpdateMoneyHold(int money)
    {
        PlayerPrefs.SetInt(moneyHold_key, money);
        PlayerPrefs.Save();
    }

    public void UpdateGachaStack(int stack)
    {
        PlayerPrefs.SetInt(gachaStack_key, stack);        
        PlayerPrefs.Save();
    }

    public void UpdateCollectedFigureIndex(int[] indexArray)
    {
        PlayerPrefsX.SetIntArray(collectedFigure_key, indexArray);
    }
}
