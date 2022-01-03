using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseBtn : MonoBehaviour
{
    [SerializeField] GameObject targetWindow;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { CloseWindow(); });
    }

    void CloseWindow()
    {
        Managers.Sound.PlaySFX(Define.SFX.Click);
        targetWindow.SetActive(false);
    }
}
