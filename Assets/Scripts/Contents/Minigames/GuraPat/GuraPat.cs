using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuraPat : BaseMinigame
{
    [SerializeReference] Button patButton;
    [SerializeReference] GameObject guraDefault;
    [SerializeReference] GameObject guraActive;

    public override void OnEnable()
    {
        guraDefault.SetActive(true);
        guraActive.SetActive(false);
        patButton.gameObject.SetActive(true);
        patButton.onClick.AddListener(PatGura);
        Managers.Sound.PlayBGM(Define.BGM.GuraPat);
    }

    public void PatGura()
    {
        guraDefault.SetActive(false);
        guraActive.SetActive(true);
        patButton.gameObject.SetActive(false);
        Managers.Sound.PlaySFX(Define.SFX.GuraPat_Pat);
        potIsSuccess.SetBool("is Game Success", true);
        minigameScene.minigameSuccessCount += Define.MINIGAME_SUCCESS_REWARD;
    }

    public override void OnDisable()
    {
        Managers.Sound.StopBGM(Define.BGM.GuraPat);
        minigameCanvas.sortingOrder = 0;
    }
}
