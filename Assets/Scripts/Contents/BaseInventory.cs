using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInventory : MonoBehaviour
{
    // Git Test
    int slotAmount;

    public List<GameObject> slots = new List<GameObject>();
    public Dictionary<int, FigureItem> items = new Dictionary<int, FigureItem>();

    [SerializeField] GameObject innerSpace;

    static public GameObject lastSeleted;

    void Awake()
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

    public abstract void AddItem();
}
