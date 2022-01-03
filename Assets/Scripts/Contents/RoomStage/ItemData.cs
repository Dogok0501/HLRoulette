using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemData : MonoBehaviour, ISelectHandler
{
    public FigureItem item;
    [SerializeField] IInventory inventory;
    [SerializeField] EventSystem eventSystem;

    public void Start()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        inventory = transform.parent.parent.parent.GetComponent<IInventory>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        IInventory.lastSeleted = eventSystem.currentSelectedGameObject;
    }
}
