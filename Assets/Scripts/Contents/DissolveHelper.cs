using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DissolveHelper : MonoBehaviour
{
    Transform u_Char;
    Transform[] children;

    float splitValue = 0;
    Figure figure;

    private void Start()
    {       
        figure = GetComponent<Figure>();
    }

    public IEnumerator Appearing(float dissolveTime)
    {
        u_Char = transform.GetChild(1);
        children = new Transform[u_Char.childCount];
        splitValue = 0f;

        for (int i = 0; i < u_Char.childCount; i++)
        {
            children[i] = u_Char.GetChild(i);
        }

        while (splitValue < 1.5)
        {
            foreach (Transform child in children)
            {
                foreach (Material mat in child.GetComponent<Renderer>().materials)
                {
                    mat.SetFloat("_SplitValue", splitValue);
                    splitValue += dissolveTime;
                }
            }
            yield return null;
        }
        yield return Managers.Coroutine.WaitForSecondsEx(2f);
    }

    public IEnumerator Dissolving(float dissolveTime)
    {
        u_Char = transform.GetChild(1);
        children = new Transform[u_Char.childCount];
        splitValue = 1f;

        for (int i = 0; i < u_Char.childCount; i++)
        {
            children[i] = u_Char.GetChild(i);
        }

        while (splitValue > 0)
        {
            foreach (Transform child in children)
            {
                foreach (Material mat in child.GetComponent<Renderer>().materials)
                {
                    splitValue -= dissolveTime;
                    mat.SetFloat("_SplitValue", splitValue);
                }
            }
            yield return null;
        }
    }
}
