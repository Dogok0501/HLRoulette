using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingAsyncManager : MonoBehaviour
{
    public Image progressFill;
    public static Define.Scene sceneType;

    private void Start()
    {
        StartCoroutine(LoadAsynchronously());
    }

    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public static void LoadScene(Define.Scene type)
    {
        Managers.Clear();
        sceneType = type;
        SceneManager.LoadScene("Loading");
    }

    IEnumerator LoadAsynchronously()
    {
        yield return null;

        AsyncOperation operation = SceneManager.LoadSceneAsync(GetSceneName(sceneType));
        operation.allowSceneActivation = false;

        float timer = 0.0f;
        while (!operation.isDone)
        {
            yield return null;

            timer += Time.deltaTime;

            if (operation.progress < 0.9f)
            {
                progressFill.fillAmount = Mathf.Lerp(progressFill.fillAmount, operation.progress, timer);
                if (progressFill.fillAmount >= operation.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressFill.fillAmount = Mathf.Lerp(progressFill.fillAmount, 1f, timer);

                if (progressFill.fillAmount == 1.0f)
                {
                    yield return Managers.Coroutine.WaitForSecondsEx(5f);
                    Managers.Sound.StopBGM(Define.BGM.Loading);
                    Managers.Clear();
                    operation.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
