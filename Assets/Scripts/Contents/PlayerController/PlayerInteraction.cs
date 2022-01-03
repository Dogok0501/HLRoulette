using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] GameObject interactionDoor;
    [SerializeField] GameObject interactionMinigame;
    [SerializeField] GameObject interactionInventory;
    [SerializeField] GameObject interactionDanceButton;

    void Update()
    {
        GetTouchPosition();
        KeepShootingRay();
    }

    void GetTouchPosition()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        if (Input.GetMouseButton(0))
        {
            Vector2 touchPosition = Input.mousePosition;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);

            if (Physics.Raycast(ray, out hit, Define.INTERACTION_DISTANCE))
            {
                if (hit.transform.GetComponent<BaseInteractable>() != null)
                {
                    hit.transform.GetComponent<BaseInteractable>().Interact();
                }
            }
        }
#else        
        if(Input.touchCount > 0)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;
                                
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);

            if (Physics.Raycast(ray, out hit, Define.INTERACTION_DISTANCE))
            {
                if (hit.transform.gameObject.GetComponent<BaseInteractable>() != null)
                {
                    hit.transform.gameObject.GetComponent<BaseInteractable>().Interact();
                }
            }
        }
#endif
    }

    void KeepShootingRay()
    {
        RaycastHit hit;
               
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Define.INTERACTION_DISTANCE))
        {
            switch (hit.transform.tag)
            {
                case "InteractionDoor":
                    interactionDoor.GetComponent<BaseInteractable>().startBlink = true;
                    break;

                case "InteractionMinigame":
                    interactionMinigame.GetComponent<BaseInteractable>().startBlink = true;
                    break;

                case "InteractionInventory":
                    interactionInventory.GetComponent<BaseInteractable>().startBlink = true;
                    break;

                case "InteractionDanceButton":
                    interactionDanceButton.GetComponent<BaseInteractable>().startBlink = true;
                    break;

                default:
                    interactionDoor.GetComponent<BaseInteractable>().startBlink = false;
                    interactionMinigame.GetComponent<BaseInteractable>().startBlink = false;
                    interactionInventory.GetComponent<BaseInteractable>().startBlink = false;
                    interactionDanceButton.GetComponent<BaseInteractable>().startBlink = false;
                    break;
            }     
        }        
    }
}
