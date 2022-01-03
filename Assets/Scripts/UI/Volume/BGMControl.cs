using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMControl : MonoBehaviour, ISoundControl
{
    Slider bgmSlider;
    Toggle muteToggle;

    private void Start()
    {
        bgmSlider = GetComponentInChildren<Slider>();
        muteToggle = GetComponentInChildren<Toggle>();
    }

    public void SettingVolume(float volume)
    {
        Managers.Sound.SetBGMVolume(volume);

        if (volume == 0)
            muteToggle.isOn = false;
        else
            muteToggle.isOn = true;
    }

    public void OnOffSound(bool isOn)
    {
        if (!isOn)
        {
            Managers.Sound.SetBGMVolume(0);
            bgmSlider.value = 0;
        }            
    }    
}
