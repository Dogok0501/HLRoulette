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
            items.Add(i, new FigureItem(-1, null, null, -1));                 // ��� ������ Dictionary�� �� -1�� index���� �ο������� �� ���������� ����
            slots.Add(Instantiate(Managers.Resource.GetSlotPrefab()));        // ��� �κ��丮 ���� List�� �κ��丮 ���� �������� ����            
            slots[i].transform.SetParent(innerSpace.transform);               // ������ slot���� slotPanel�� �θ�� ���������� slotPanel�� Grid Layout Group�� �����س��� ��ġ�� ���ĵ�.
        }

        AddItem();
    }

    public abstract void AddItem();
}
