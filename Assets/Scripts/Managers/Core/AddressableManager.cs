using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddressableManager : MonoBehaviour
{
    public static AsyncOperationHandle<GameObject>[] handle;
    string[] LableForBundleDown;
    int counter;

    private static AddressableManager adrs_Instance;
    public static AddressableManager Instance { get { Init(); return adrs_Instance; } }

    [SerializeField] Slider downloadSlider;
    [SerializeField] TextMeshProUGUI downloadPerText;

    private void Awake()
    {
        Init();
    }

    private static void Init()
    {
        if (adrs_Instance == null)
        {
            GameObject obj = GameObject.Find("@AddressableManager");
            if (obj == null)
            {
                obj = new GameObject { name = "@AddressableManager" };
                obj.AddComponent<Managers>();
            }

            DontDestroyOnLoad(obj);

            adrs_Instance = obj.GetComponent<AddressableManager>();
        }
    }


    void Start()
    {
        LableForBundleDown = new string[Managers.Data.figureDataDict.Count]; 

        for (int i = 0; i < Managers.Data.figureDataDict.Count; i++)
        {
            LableForBundleDown[i] = Managers.Data.figureDataDict[i + 1000].name.ToString();
        }

        counter = 0;
        handle = new AsyncOperationHandle<GameObject>[LableForBundleDown.Length];
        downloadPerText.text = Math.Round(downloadSlider.value / LableForBundleDown.Length * 100.0f, 2).ToString() + " %";
    }

    public void ClickBundleDown()
    {
        StartCoroutine(BundleDown());        
    }

    IEnumerator BundleDown()
    {    
        for (int i = 0; i < LableForBundleDown.Length; i++)
        {
            handle[i] = Addressables.LoadAssetAsync<GameObject>(LableForBundleDown[i]);
            yield return new WaitUntil(() => handle[i].IsDone);
            handle[i].Completed += (AsyncOperationHandle<GameObject> obj) => { handle[i] = obj; counter++; Debug.Log(LableForBundleDown[i] + "다운로드 완료!"); downloadSlider.value++; downloadPerText.text = Math.Round(downloadSlider.value / LableForBundleDown.Length * 100.0f, 2).ToString() + " %"; };

            yield return new WaitForSeconds(1.0f);
        }

        if(counter == LableForBundleDown.Length)
                SceneManager.LoadSceneAsync("Title");
    }
}
