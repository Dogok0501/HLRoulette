using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoomInventory : BaseInventory
{
    [SerializeField] Canvas inventoryCanvas;
    [SerializeField] Canvas UICanvas;
                
    [SerializeField] StageFigureSpawner stageFigureSpawner;

    void Start()
    {
        lastSeleted = null;
        transform.parent.gameObject.SetActive(false);
    }

    public override void AddItem()
    {
        FigureItem[] itemToAdd = new FigureItem[8];

        for (int j = 0; j < 8; j++)
        {
            itemToAdd[j] = Managers.Data.figureDataDict[j + 1000];

            for (int i = 0; i < items.Count; i++) // 좌측 상단 슬롯부터 차례대로 검사
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

    public void ConfirmButton()
    {
        if(lastSeleted != null)
        {
            stageFigureSpawner.GetFigureFromInventory(lastSeleted.GetComponent<ItemData>().item.index);
            CloseInventory();
        }            
        else
        {
            CloseInventory();
        }
    }

    public void CancleButton()
    {
        lastSeleted = null;
        CloseInventory();
    }

    void CloseInventory()
    {
        Managers.Sound.PlaySFX(Define.SFX.Click);
        UICanvas.gameObject.SetActive(true);
        inventoryCanvas.gameObject.SetActive(false);
    }
}
