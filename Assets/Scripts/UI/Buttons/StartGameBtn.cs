using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameBtn : MonoBehaviour
{
    [SerializeField] GameObject fadeinImage;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { ActiveFadeInImage(); });
    }

    void ActiveFadeInImage()
    {
        fadeinImage.SetActive(true);

        Managers.Sound.PlaySFX(Define.SFX.Click);

        Invoke("LoadMainScene", Define.FADE_IN_OUT_TIME + 1.0f);
    }

    private void LoadMainScene()
    {
        LoadingAsyncManager.LoadScene(Define.Scene.Main);
    }
}
