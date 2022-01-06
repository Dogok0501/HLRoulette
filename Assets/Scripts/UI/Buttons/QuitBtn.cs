using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitBtn : BaseBtn
{
    protected override void ButtonFunction()
    {
        Managers.Sound.PlaySFX(Define.SFX.Click);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
