using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchRoomDoor : MonoBehaviour
{
    GameObject roomDoor;
    [SerializeField] GameObject fadeinImage;

    private void Start()
    {
        Init();
        TouchUpdate();
    }

    private void Init()
    {
        roomDoor = transform.gameObject;
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
                    if (hit.transform.gameObject == roomDoor)
                    {
                        Managers.Sound.PlaySFX(Define.SFX.Click);
                        fadeinImage.SetActive(true);

                        yield return Managers.Coroutine.WaitForSecondsEx(2f);
                        LoadingAsyncManager.LoadScene(Define.Scene.Room);
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
                    if (hit.transform.gameObject == roomDoor)
                    {
                        Managers.Sound.PlaySFX(Define.SFX.Click);
                        fadeinImage.SetActive(true);
                        
                        yield return Managers.Coroutine.WaitForSecondsEx(2f);
                        LoadingAsyncManager.LoadScene(Define.Scene.Room);
                    }
                }
            }
            yield return null;
        }        
#endif
    }
}
