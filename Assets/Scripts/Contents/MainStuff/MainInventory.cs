using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainInventory : IInventory
{
    int slotAmount;

    [SerializeField] GameObject innerSpace;

    List<GameObject> slots = new List<GameObject>();
    Dictionary<int, FigureItem> items = new Dictionary<int, FigureItem>();

    private void Awake()
    {
        slotAmount = 18;
        for (int i = 0; i < slotAmount; i++)
        {
            items.Add(i, new FigureItem(-1, null, null, -1));                 // ��� ������ Dictionary�� �� -1�� index���� �ο������� �� ���������� ����
            slots.Add(Instantiate(Managers.Resource.GetSlotPrefab()));        // ��� �κ��丮 ���� List�� �κ��丮 ���� �������� ����            
            slots[i].transform.SetParent(innerSpace.transform);               // ������ slot���� slotPanel�� �θ�� ���������� slotPanel�� Grid Layout Group�� �����س��� ��ġ�� ���ĵ�.
        }

        AddItem();
    }

    private void OnEnable()
    {        
        UpdateItem();
    }

    void AddItem()
    {
        FigureItem[] itemToAdd = new FigureItem[15];

        for (int j = 0; j < 15; j++)
        {
            itemToAdd[j] = Managers.Data.figureDataDict[j + 1000];

            for (int i = 0; i < 15; i++) // ���� ��� ���Ժ��� ���ʴ�� �˻�
            {
                if (items[i].index == -1)
                {
                    items[i] = itemToAdd[j];

                    GameObject itemObj = Instantiate(Managers.Resource.GetItemPrefab());
                    itemObj.GetComponent<ItemData>().item = itemToAdd[i]; // ItemData�� �ش� ������ Dictionary value ������ ����                
                    itemObj.transform.SetParent(slots[i].transform);
                    Sprite itemSprite = Resources.Load<Sprite>($"Images/FigureIcons/{itemToAdd[i].name}");
                    itemObj.transform.position = slots[i].transform.position;
                    itemObj.GetComponent<Image>().sprite = itemSprite;

                    break;
                }
            }
        }
    }    

    void UpdateItem()
    {
        for (int j = 0; j < 15; j++)
        {
            int index = slots[j].GetComponentInChildren<ItemData>().item.index;

            if (Array.IndexOf(Managers.Game.collectedFigureIndex, index) == -1)
            {
                slots[j].GetComponentInChildren<Button>().interactable = false;
            }
            else
            {
                slots[j].GetComponentInChildren<Button>().interactable = true;
            }
        }
    }
}
