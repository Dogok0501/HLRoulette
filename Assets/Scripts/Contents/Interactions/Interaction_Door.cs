using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Door : BaseInteractable
{
    [SerializeReference] GameObject fadeinImage;

    public override IEnumerator SelectObject()
    {
        Managers.Sound.PlaySFX(Define.SFX.Click);
        fadeinImage.SetActive(true);

        yield return Managers.Coroutine.WaitForSecondsEx(2f);
        LoadingAsyncManager.LoadScene(Define.Scene.Main);
    }
}
