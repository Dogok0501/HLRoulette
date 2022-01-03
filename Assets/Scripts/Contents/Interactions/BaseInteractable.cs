using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour
{    
    public bool startBlink = false;
    float speed = 1f;
    Renderer ren;
    Color originColor;

    private void Start()
    {
        ren = GetComponent<Renderer>();
        originColor = ren.material.color;
        ReachUpdate();
    }

    void ReachUpdate()
    {
        StartCoroutine(ColorLerp());
    }

    public void Interact()
    {
        StartCoroutine(SelectObject());
    }

    public IEnumerator ColorLerp()
    {
        while (true)
        {
            if (startBlink)
            {
                ren.material.color = Color.Lerp(originColor, Color.green, Mathf.PingPong(Time.time * speed, 1f));
            }
            else
            {
                ren.material.color = originColor;
            }
            yield return null;
        }
    }

    public abstract IEnumerator SelectObject();
}
