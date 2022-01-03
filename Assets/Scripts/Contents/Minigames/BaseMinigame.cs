using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMinigame : MonoBehaviour
{
    public Canvas minigameCanvas;
    public Animator potIsSuccess;
    public MinigameScene minigameScene;

    private void Awake()
    {
        minigameCanvas = GetComponentInParent<Canvas>();
    }

    public abstract void OnEnable();

    public abstract void OnDisable();
}
