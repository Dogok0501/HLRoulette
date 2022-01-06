using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainInventory : BaseInventory
{        
    private void OnEnable()
    {        
        UpdateItem();
    }

    public override void AddItem()
    {
        FigureItem[] itemToAdd = new FigureItem[Define.TOTAL_FIGURE_NUM];

        for (int j = 0; j < Define.TOTAL_FIGURE_NUM; j++)
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
