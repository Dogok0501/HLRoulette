using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : BaseScene
{
    protected override void Init()
    {
        SceneType = Define.Scene.Loading;
        Clear();
        Managers.Sound.PlayBGM(Define.BGM.Loading);
    }

    public override void Clear()
    {
        Managers.Pool.Clear();
    }    
}
