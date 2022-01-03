using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    public GameObject titleCanvas;
    public GameObject buttonCanvas;

    protected override void Init()
    {
        SceneType = Define.Scene.Title;

        //Ÿ��Ʋ ĵ���� ����
        titleCanvas.SetActive(false);
        StartCoroutine(ReActive(5f, titleCanvas));

        //��ư ĵ���� ����
        buttonCanvas.SetActive(false);
        StartCoroutine(ReActive(7f, buttonCanvas));
    }

    private IEnumerator ReActive(float sec, GameObject gameObject)
    {
        yield return Managers.Coroutine.WaitForSecondsEx(sec);

        gameObject.SetActive(true);
    }

    public override void Clear()
    {
        Managers.Pool.Clear();
    }
}
