using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoomInventory : IInventory
{
    [SerializeField] Canvas inventoryCanvas;
    [SerializeField] Canvas UICanvas;

    int slotAmount;

    [SerializeField] GameObject innerSpace;

    List<GameObject> slots = new List<GameObject>();
    Dictionary<int, FigureItem> items = new Dictionary<int, FigureItem>();
        
    [SerializeField] StageFigureSpawner stageFigureSpawner;

    private void Awake()
    {
        lastSeleted = null;
        slotAmount = 18;
        for (int i = 0; i < slotAmount; i++)
        {
            items.Add(i, new FigureItem(-1, null, null, -1));                 // 모든 아이템 Dictionary에 빈 -1의 index값을 부여함으로 빈 아이템으로 만듬
            slots.Add(Instantiate(Managers.Resource.GetSlotPrefab()));        // 모든 인벤토리 슬롯 List에 인벤토리 슬롯 프리팹을 생성            
            slots[i].transform.SetParent(innerSpace.transform);               // 생성된 slot들을 slotPanel을 부모로 설정함으로 slotPanel의 Grid Layout Group로 설정해놓은 위치로 정렬됨.
        }

        transform.parent.gameObject.SetActive(false);

        AddItem();
    }

    void AddItem()
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
