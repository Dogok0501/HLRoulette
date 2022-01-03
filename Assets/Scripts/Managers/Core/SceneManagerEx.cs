using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    public void LoadScene(Define.Scene scene)
    {
        Clear();
        SceneManager.LoadScene(Get_SceneName(scene));
    }

    public void LoadSceneAsync(Define.Scene scene)
    {
        Clear();
        SceneManager.LoadSceneAsync(Get_SceneName(scene));
    }

    private string Get_SceneName(Define.Scene type)
    {
        string name = Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
