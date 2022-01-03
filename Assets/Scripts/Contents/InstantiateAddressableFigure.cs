using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class InstantiateAddressableFigure : MonoBehaviour
{
    AsyncOperationHandle handle;
    public GameObject Prefab { get; private set; }

    void Awake()
    {
        for (int i = 1000; i < 1014; i++)
        {
            Addressables.InstantiateAsync(Managers.Data.figureDataDict[i].name + " Prefab", new Vector3(0f, 0f, 0f), Quaternion.identity).Completed += (AsyncOperationHandle<GameObject> obj) =>
            {
                handle = obj;
                Prefab = obj.Result;
            };
        }            
    }
}
