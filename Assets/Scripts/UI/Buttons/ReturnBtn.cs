using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnBtn : MonoBehaviour
{
    [SerializeField] GameObject fadeinImage;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { StarterReturnToRoomScene(); });
    }

    void StarterReturnToRoomScene()
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
