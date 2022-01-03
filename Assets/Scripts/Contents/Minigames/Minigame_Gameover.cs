using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame_Gameover : MonoBehaviour
{
    [SerializeField] GameObject fadeinImage;

    private void OnEnable()
    {
        Managers.Sound.PlaySFX(Define.SFX.Minigame_GameOver);
        StartCoroutine(BackToRoomScene());
    }

    IEnumerator BackToRoomScene()
    {
        yield return Managers.Coroutine.WaitForSecondsEx(3f);
        fadeinImage.SetActive(true);
        yield return Managers.Coroutine.WaitForSecondsEx(2f);
        LoadingAsyncManager.LoadScene(Define.Scene.Room);
    }
}
