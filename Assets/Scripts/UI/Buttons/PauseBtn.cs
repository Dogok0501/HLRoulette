using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseBtn : BaseBtn
{
    [SerializeField] GameObject targetWindow;

    protected override void ButtonFunction()
    {
        Managers.Sound.PlaySFX(Define.SFX.Click);
        targetWindow.SetActive(true);
        Time.timeScale = 0;
    }
}
