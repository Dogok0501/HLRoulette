using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchGachaLever : MonoBehaviour
{
    GameObject gachaLever;

    private void Start()
    {
        Init();
        TouchUpdate();
    }

    private void Init()
    {
        gachaLever = transform.gameObject;
    }

    private void TouchUpdate()
    {
        StartCoroutine(GetTouchPosition());
    }

    private IEnumerator GetTouchPosition()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        while (!Managers.Game.isGameOver)
        {
            if (Input.GetMouseButton(0) && !Managers.Game.isGachaing)
            {
                Vector2 touchPosition = Input.mousePosition;

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(touchPosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.transform.gameObject == gachaLever)
                    {

                        Managers.Sound.PlaySFX(Define.SFX.Click);
                        Managers.Sound.PlaySFX(Define.SFX.LeverSpin);
                        StartCoroutine(LeverSpin());
                        Managers.Game.isPull = !Managers.Game.isPull;
                    }
                }
            }
            yield return null;
        }
#else        
        while(!Managers.Game.isGameOver)
        {
            if(Input.touchCount > 0 && !Managers.Game.isGachaing)
            {
                Vector2 touchPosition = Input.GetTouch(0).position;
                                
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(touchPosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.transform.gameObject == gachaLever)
                    {
                        Managers.Sound.PlaySFX(Define.SFX.Click);
                        Managers.Sound.PlaySFX(Define.SFX.LeverSpin);
                        StartCoroutine(LeverSpin());
                        Managers.Game.isPull = !Managers.Game.isPull;
                    }
                }
            }
            yield return null;
        }        
#endif
    }

    private IEnumerator LeverSpin()
    {
        Quaternion defaultRot = gachaLever.transform.rotation;

        float elapsedTime = 0f;
        float waitTime = 4f;

        while (elapsedTime < waitTime)
        {
            gachaLever.transform.Rotate(0, 0, 60 * (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        gachaLever.transform.rotation = defaultRot;
    }
}
