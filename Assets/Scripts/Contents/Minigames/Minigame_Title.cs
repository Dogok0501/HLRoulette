using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame_Title : MonoBehaviour
{
    Animator anim;
    [SerializeReference] Button startButton;
    [SerializeReference] GameObject minigameOpening;

    private void OnEnable()
    {
        startButton.gameObject.SetActive(false);
        anim = GetComponent<Animator>();
        Managers.Sound.PlaySFX(Define.SFX.Minigame_TitleDrop);        
    }

    public void LoopingTitle()
    {
        startButton.gameObject.SetActive(true);
        startButton.onClick.AddListener(StartGameButton);
        Managers.Sound.PlayBGM(Define.BGM.Minigame_TitleLoop);
    }

    public void StartGameButton()
    {
        anim.SetTrigger("Game Start Trigger");
        Managers.Sound.StopBGM(Define.BGM.Minigame_TitleLoop);
        Managers.Sound.PlaySFX(Define.SFX.Minigame_TitleStart);
        StartCoroutine(ToMinigameOpening());
    }

    IEnumerator ToMinigameOpening()
    {
        startButton.gameObject.SetActive(false);

        yield return Managers.Coroutine.WaitForSecondsEx(1f);

        minigameOpening.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
