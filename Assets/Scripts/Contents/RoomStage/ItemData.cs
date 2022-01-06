using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemData : MonoBehaviour, ISelectHandler
{
    public FigureItem item;
    [SerializeField] BaseInventory inventory;
    [SerializeField] EventSystem eventSystem;

    public void Start()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        inventory = transform.parent.parent.parent.GetComponent<BaseInventory>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        BaseInventory.lastSeleted = eventSystem.currentSelectedGameObject;
    }
}
