using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnBtn : BaseBtn
{
    [SerializeField] GameObject fadeinImage;

    protected override void ButtonFunction()
    {
        StartCoroutine(ReturnToRoomScene());
    }

    IEnumerator ReturnToRoomScene()
    {
        fadeinImage.SetActive(true);
        yield return Managers.Coroutine.WaitForSecondsEx(2f);
        Managers.Sound.StopBGM(Define.BGM.Minigame_TitleLoop);
        LoadingAsyncManager.LoadScene(Define.Scene.Room);
    }

    
}
