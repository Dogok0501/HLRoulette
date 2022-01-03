using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeBtn : MonoBehaviour
{
    [SerializeField] GameObject targetWindow;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { ResumeGame(); });
    }

    void ResumeGame()
    {
        Managers.Sound.PlaySFX(Define.SFX.Click);
        targetWindow.SetActive(false);
        Time.timeScale = 1;
    }    
}
