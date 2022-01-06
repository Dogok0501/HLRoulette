using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetDataBtn : BaseBtn
{
    [SerializeField] GameObject fadeinImage;

    protected override void ButtonFunction()
    {
        StartCoroutine(ResetAllData());
    }

    IEnumerator ResetAllData()
    {
        PlayerPrefs.DeleteAll();
        Managers.Sound.PlaySFX(Define.SFX.Click);
        fadeinImage.SetActive(true);
        yield return Managers.Coroutine.WaitForSecondsEx(2f);
        LoadingAsyncManager.LoadScene(Define.Scene.Title);
    }    
}
