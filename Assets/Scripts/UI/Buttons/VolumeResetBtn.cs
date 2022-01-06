using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeResetBtn : BaseBtn
{
    [SerializeField] Slider[] sliders;

    protected override void ButtonFunction()
    {
        Managers.Sound.SetSFXVolume(1);
        Managers.Sound.SetBGMVolume(1);

        sliders[0].value = 1;
        sliders[1].value = 1;
    }
}
