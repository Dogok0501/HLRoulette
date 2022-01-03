using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame_Opening : MonoBehaviour
{
    [SerializeReference] Transform[] openingSequence;
    [SerializeReference] GameObject pot;
    int imageNum;

    private void OnEnable()
    {
        Managers.Sound.PlaySFX(Define.SFX.Minigame_OP);
        imageNum = 0;
    }

    void DisableImage()
    {
        openingSequence[imageNum].gameObject.SetActive(false);
        openingSequence[imageNum+1].gameObject.SetActive(true);
        imageNum++;
    }

    void ToPot()
    {
        pot.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
