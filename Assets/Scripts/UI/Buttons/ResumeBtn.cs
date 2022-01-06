using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeBtn : BaseBtn
{
    [SerializeField] GameObject targetWindow;

    protected override void ButtonFunction()
    {
        Managers.Sound.PlaySFX(Define.SFX.Click);
        targetWindow.SetActive(false);
        Time.timeScale = 1;
    }
}
