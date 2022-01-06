using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseBtn : MonoBehaviour
{
    [SerializeField] GameObject targetWindow;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { PauseGame(); });
    }

    void PauseGame()
    {
        Managers.Sound.PlaySFX(Define.SFX.Click);
        targetWindow.SetActive(true);
        Time.timeScale = 0;
    }
}
