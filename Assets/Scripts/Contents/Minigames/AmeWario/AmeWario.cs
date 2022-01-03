using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmeWario : BaseMinigame
{
    AmeWarioBox box;

    public override void OnEnable()
    {
        box = GetComponentInChildren<AmeWarioBox>();
        box.gameObject.SetActive(true);

        Managers.Sound.PlayBGM(Define.BGM.AmeWario);
    }

    public void BoxBreak()
    {
        potIsSuccess.SetBool("is Game Success", true);
        minigameScene.minigameSuccessCount += Define.MINIGAME_SUCCESS_REWARD;
    }

    public override void OnDisable()
    {
        Managers.Sound.StopBGM(Define.BGM.AmeWario);
        minigameCanvas.sortingOrder = 0;
    }    
}
