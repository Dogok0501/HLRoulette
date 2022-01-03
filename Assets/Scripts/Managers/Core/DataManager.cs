using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{   
    public Dictionary<int, FigureItem> figureDataDict { get; private set; } = new Dictionary<int, FigureItem>();

    public void Init()
    {
        figureDataDict = LoadJson<FigureItemData, int, FigureItem>(Define.DataType.FigureItemData).MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(Define.DataType dataType) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.dataTextAsset[dataType];
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
