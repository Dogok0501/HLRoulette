using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScene : BaseScene
{
    protected override void Init()
    {
        SceneType = Define.Scene.Room;

        Managers.Sound.PlayBGM(Define.BGM.Main);
    }

    public override void Clear()
    {
        Managers.Pool.Clear();
        Managers.Sound.StopBGM(Define.BGM.Main);
        Managers.Sound.StopBGM(Define.BGM.Room);
    }
}
