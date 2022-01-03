using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameScene : BaseScene
{
    [SerializeReference] Transform titlePanel;
    public bool isMinigameOver = false;
    public int minigameSuccessCount = 0;

    protected override void Init()
    {
        SceneType = Define.Scene.Minigame;
        if (titlePanel.gameObject.activeSelf == false)
            titlePanel.gameObject.SetActive(true);
    }

    public void UpdateMoney()
    {
        Managers.Game.moneyHold += minigameSuccessCount;
        Managers.SaveLoad.UpdateMoneyHold(Managers.Game.moneyHold);
    }

    public override void Clear()
    {
        Managers.Pool.Clear();
    }
}
