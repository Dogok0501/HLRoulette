using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseBtn : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { ButtonFunction(); });
    }

    protected abstract void ButtonFunction();
}

