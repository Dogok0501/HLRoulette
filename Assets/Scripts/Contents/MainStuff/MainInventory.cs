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
            items.Add(i, new FigureItem(-1, null, null, -1));                 // 모든 아이템 Dictionary에 빈 -1의 index값을 부여함으로 빈 아이템으로 만듬
            slots.Add(Instantiate(Managers.Resource.GetSlotPrefab()));        // 모든 인벤토리 슬롯 List에 인벤토리 슬롯 프리팹을 생성            
            slots[i].transform.SetParent(innerSpace.transform);               // 생성된 slot들을 slotPanel을 부모로 설정함으로 slotPanel의 Grid Layout Group로 설정해놓은 위치로 정렬됨.
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

            for (int i = 0; i < 15; i++) // 좌측 상단 슬롯부터 차례대로 검사
            {
                if (items[i].index == -1)
                {
                    items[i] = itemToAdd[j];

                    GameObject itemObj = Instantiate(Managers.Resource.GetItemPrefab());
                    itemObj.GetComponent<ItemData>().item = itemToAdd[i]; // ItemData에 해당 아이템 Dictionary value 정보를 전달                
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
