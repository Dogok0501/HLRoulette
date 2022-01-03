using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEffectInput : MonoBehaviour
{
    GameObject effectPrefab;

    void Start()
    {
        effectPrefab = Managers.Resource.GetMouseEffectPrefab();
        CreatePool();
        MouseEffectLoop();
    }

    private void MouseEffectLoop()
    {
        StartCoroutine(MouseEffect());
    }

    private void CreatePool()
    {
        Managers.Pool.CreatePool(effectPrefab);
    }

    IEnumerator MouseEffect()
    {
        float elapsedTime = 0f;
        float waitTime = 0.05f;

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        while (!Managers.Game.isGameOver)
        {           
            if (Input.GetMouseButton(0) && elapsedTime >= waitTime)
            {
                MouseEffectCreate();
                elapsedTime = 0f;
            }
            elapsedTime += Time.deltaTime;

            yield return null;
        }
#else
        while(!Managers.Game.isGameOver)
        {
            if(Input.touchCount > 0 && elapsedTime >= waitTime)
            {
                MouseEffectCreate();
                elapsedTime = 0f;
            }
            elapsedTime += Time.deltaTime;

            yield return null;
        }
#endif
    }

    private void MouseEffectCreate()
    {
        Vector3 tempMousePosition = Input.mousePosition;
        tempMousePosition.z = 0;
        for(int i = 0; i < 2; i++)
        {
            Poolable effect = Managers.Game.Instantiate(effectPrefab, this.transform);
            effect.myTransform.position = tempMousePosition;
        }        
    }
}