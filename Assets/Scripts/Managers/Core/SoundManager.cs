using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager
{
    public Dictionary<Define.BGM, AudioSource> bgmAudioSources = new Dictionary<Define.BGM, AudioSource>();
    public Dictionary<Define.SFX, AudioSource> sfxAudioSources = new Dictionary<Define.SFX, AudioSource>();

    public bool canSFX = true;

    public void Init()
    {
        // @Sound 와 하위 BGM, SFX AudioSource 오브젝트 생성 시작
        GameObject obj = GameObject.Find("@Sounds");
        if(obj == null)
        {
            obj = new GameObject { name = "@Sounds" };
            UnityEngine.Object.DontDestroyOnLoad(obj);

            GameObject bgm_obj = new GameObject { name = "BGM" };
            GameObject sfx_obj = new GameObject { name = "SFX" };

            foreach (Define.BGM bgm in Enum.GetValues(typeof(Define.BGM)))
            {
                bgmAudioSources[bgm] = bgm_obj.AddComponent<AudioSource>();
                bgm_obj.transform.parent = obj.transform;
            }

            foreach (Define.SFX sfx in Enum.GetValues(typeof(Define.SFX)))
            {
                sfxAudioSources[sfx] = sfx_obj.AddComponent<AudioSource>();
                sfx_obj.transform.parent = obj.transform;
            }
        }                
        // @Sound 와 하위 BGM, SFX AudioSource 오브젝트 생성 끝

        foreach (Define.BGM bgm in Enum.GetValues(typeof(Define.BGM)))
            SetBGMPlayer(bgm);

        foreach (Define.SFX sfx in Enum.GetValues(typeof(Define.SFX)))
            SetSFXPlayer(sfx);
    }

    private void SetBGMPlayer(Define.BGM bgm)
    {
        bgmAudioSources[bgm].clip = Managers.Resource.bgmAudioClips[bgm];
        bgmAudioSources[bgm].loop = true;
        bgmAudioSources[bgm].playOnAwake = true;
    }

    private void SetSFXPlayer(Define.SFX sfx)
    {
        sfxAudioSources[sfx].clip = Managers.Resource.sfxAudioClips[sfx];
        sfxAudioSources[sfx].loop = false;
        sfxAudioSources[sfx].playOnAwake = false;
    }

    public void PlayBGM(Define.BGM bgm)
    {
        bgmAudioSources[bgm].Play();
    }

    public void PlaySFX(Define.SFX sfx)
    {
        if(canSFX)
            sfxAudioSources[sfx].Play();
    }

    public void StopBGM(Define.BGM bgm)
    {
        bgmAudioSources[bgm].Stop();
    }

    public void StopSFX(Define.SFX sfx)
    {
        sfxAudioSources[sfx].Stop();
    }

    public void SetBGMVolume(float _volume)
    {
        foreach (Define.BGM bgm in Enum.GetValues(typeof(Define.BGM)))
            bgmAudioSources[bgm].volume = _volume;
    }

    public void SetSFXVolume(float _volume)
    {
        foreach (Define.SFX sfx in Enum.GetValues(typeof(Define.SFX)))
            sfxAudioSources[sfx].volume = _volume;
    }
}
