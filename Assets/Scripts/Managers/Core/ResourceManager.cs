using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class ResourceManager
{
    public void Init()
    {
        LoadData();
        LoadPrefab();
        LoadAudio();
    }

    #region data

    public Dictionary<Define.DataType, TextAsset> dataTextAsset = new Dictionary<Define.DataType, TextAsset>();

    private void LoadData()
    {
        foreach (Define.DataType dataType in Enum.GetValues(typeof(Define.DataType)))
        {
            dataTextAsset[dataType] = Resources.Load<TextAsset>("Data/" + dataType.ToString());
        }            
    }

    #endregion

    #region prefab

    GameObject mouseEffectPrefab;
    GameObject popupTextPrefab;
    GameObject ballPrefab;
    GameObject[] inventoryPrefabs = new GameObject[2];

    public GameObject GetMouseEffectPrefab() { return mouseEffectPrefab; }
    public GameObject GetTextPrefab() { return popupTextPrefab; }
    public GameObject GetBallPrefab() { return ballPrefab; }

    public GameObject GetSlotPrefab() { return inventoryPrefabs[0]; }
    public GameObject GetItemPrefab() { return inventoryPrefabs[1]; }

    private void LoadPrefab()
    {        
        mouseEffectPrefab = Resources.Load<GameObject>("Prefabs/Effects/MouseEffect/Star");
        popupTextPrefab = Resources.Load<GameObject>("Prefabs/PopupTextPrefab/PopupText");
        ballPrefab = Resources.Load<GameObject>("Prefabs/Gachapon/Ball");

        inventoryPrefabs[0] = Resources.Load<GameObject>("Prefabs/UI/Inventory/Slot");
        inventoryPrefabs[1] = Resources.Load<GameObject>("Prefabs/UI/Inventory/Item");
    }

    #endregion

    #region audio

    public Dictionary<Define.BGM, AudioClip> bgmAudioClips = new Dictionary<Define.BGM, AudioClip>();
    public Dictionary<Define.SFX, AudioClip> sfxAudioClips = new Dictionary<Define.SFX, AudioClip>();    

    private void LoadAudio()
    {
        foreach (Define.BGM bgm in Enum.GetValues(typeof(Define.BGM)))
            bgmAudioClips[bgm] = Resources.Load<AudioClip>("Sounds/BGMs/" + bgm.ToString());

        foreach (Define.SFX sfx in Enum.GetValues(typeof(Define.SFX)))
            sfxAudioClips[sfx] = Resources.Load<AudioClip>("Sounds/SFXs/" + sfx.ToString());
    }

    #endregion   
}
