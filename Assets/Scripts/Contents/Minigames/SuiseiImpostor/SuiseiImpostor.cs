using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuiseiImpostor : BaseMinigame
{
    [SerializeReference] Button killButton;
    [SerializeReference] Animator suiseiPortrait;
    [SerializeReference] Animator guraCrew;
    [SerializeReference] GameObject guraPortrait;

    public override void OnEnable()
    {
        guraPortrait.SetActive(false);

        killButton.gameObject.SetActive(true);
        killButton.onClick.AddListener(KillGura);

        Managers.Sound.PlayBGM(Define.BGM.GuraPat);
    }

    public void KillGura()
    {
        killButton.gameObject.SetActive(false);

        suiseiPortrait.SetTrigger("is Kill");
        guraCrew.SetTrigger("is Killed");
        guraPortrait.SetActive(true);
        Managers.Sound.PlaySFX(Define.SFX.SuiseiImpostor_Kill);

        potIsSuccess.SetBool("is Game Success", true);
        minigameScene.minigameSuccessCount += Define.MINIGAME_SUCCESS_REWARD;
    }

    public override void OnDisable()
    {
        Managers.Sound.StopBGM(Define.BGM.GuraPat);
        minigameCanvas.sortingOrder = 0;
    }
}
