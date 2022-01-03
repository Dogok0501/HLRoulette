using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXControl : MonoBehaviour, ISoundControl
{
    Slider sfxSlider;
    Toggle muteToggle;

    private void Start()
    {
        sfxSlider = GetComponentInChildren<Slider>();
        muteToggle = GetComponentInChildren<Toggle>();
    }

    public void SettingVolume(float volume)
    {
        Managers.Sound.SetSFXVolume(volume);

        if (volume == 0)
            muteToggle.isOn = false;
        else
            muteToggle.isOn = true;
    }

    public void OnOffSound(bool isOn)
    {
        if (isOn)
        {
            Managers.Sound.SetSFXVolume(1);
            sfxSlider.value = 1;
        }
        else
        {
            Managers.Sound.SetSFXVolume(0);
            sfxSlider.value = 0;
        }

    }
}
