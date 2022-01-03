using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    public Transform[] stand;

    protected override void Init()
    {
        SceneType = Define.Scene.Main;

        Managers.Sound.PlayBGM(Define.BGM.Main);
    }    

    public override void Clear()
    {
        Managers.Pool.Clear();
        Managers.Sound.StopBGM(Define.BGM.Main);
    }    
}
