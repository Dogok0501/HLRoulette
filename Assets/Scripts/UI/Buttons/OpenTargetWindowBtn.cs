using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenTargetWindowBtn : MonoBehaviour
{
    [SerializeField] GameObject targetWindow;

    void Start()
    {        
        GetComponent<Button>().onClick.AddListener(delegate { ActiveWindow(); });
    }

    void ActiveWindow()
    {
        Managers.Sound.PlaySFX(Define.SFX.Click);
        targetWindow.SetActive(true);        
    }
}
