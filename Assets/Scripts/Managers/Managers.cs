using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class Managers : MonoBehaviour
{
    private static Managers s_Instance;
    public static Managers Instance { get { Init(); return s_Instance; } }

    #region managers

    SaveLoadManager saveload = new SaveLoadManager();
    public static SaveLoadManager SaveLoad { get { return Instance.saveload; } }    
       
    ResourceManager resource = new ResourceManager();
    public static ResourceManager Resource { get { return Instance.resource; } }

    SoundManager sound = new SoundManager();
    public static SoundManager Sound { get { return Instance.sound; } }

    DataManager data = new DataManager();
    public static DataManager Data { get { return Instance.data; } }

    PoolManager pool = new PoolManager();
    public static PoolManager Pool { get { return Instance.pool; } }

    GameManager game = new GameManager();
    public static GameManager Game { get { return Instance.game; } }

    SceneManagerEx scene = new SceneManagerEx();
    public static SceneManagerEx Scene { get { return Instance.scene; } }

    CoroutineManager coroutine = new CoroutineManager();
    public static CoroutineManager Coroutine { get { return Instance.coroutine; } }

    #endregion

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
    }

    private void Start()
    {
        Init(); 
    }

    private static void Init()
    {
        if (s_Instance == null)
        {
            GameObject obj = GameObject.Find("@Managers");
            if (obj == null)
            {
                obj = new GameObject { name = "@Managers" };
                obj.AddComponent<Managers>();
            }

            DontDestroyOnLoad(obj);

            s_Instance = obj.GetComponent<Managers>();

            s_Instance.saveload.Init();
            s_Instance.resource.Init();
            s_Instance.sound.Init();
            s_Instance.data.Init();
            s_Instance.pool.Init();
            s_Instance.game.Init();
        }
    }

    public static void Clear()
    {
        Pool.Clear();
        Scene.Clear();
    }
}
