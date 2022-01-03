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
            items.Add(i, new FigureItem(-1, null, null, -1));                 // ��� ������ Dictionary�� �� -1�� index���� �ο������� �� ���������� ����
            slots.Add(Instantiate(Managers.Resource.GetSlotPrefab()));        // ��� �κ��丮 ���� List�� �κ��丮 ���� �������� ����            
            slots[i].transform.SetParent(innerSpace.transform);               // ������ slot���� slotPanel�� �θ�� ���������� slotPanel�� Grid Layout Group�� �����س��� ��ġ�� ���ĵ�.
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

            for (int i = 0; i < items.Count; i++) // ���� ��� ���Ժ��� ���ʴ�� �˻�
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
