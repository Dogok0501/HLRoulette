using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region FigureItem

[Serializable]
public class FigureItem
{
    public int index;
    public string name;
    public string rarity;
    public int sell_earn;

    public FigureItem(int index, string name, string rarity, int sell_earn)
    {
        this.index = index;
        this.name = name;
        this.rarity = rarity;
        this.sell_earn = sell_earn;
    }
}

[Serializable]
public class FigureItemData : ILoader<int, FigureItem>
{
    public List<FigureItem> figureItems = new List<FigureItem>();

    public Dictionary<int, FigureItem> MakeDict()
    {        
        Dictionary<int, FigureItem> dict = new Dictionary<int, FigureItem>();
        foreach (FigureItem _figureItem in figureItems)
        {
            dict.Add(_figureItem.index, _figureItem);
        }            
        return dict;
    }
}

#endregion