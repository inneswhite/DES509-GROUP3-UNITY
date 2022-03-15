using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public int itemId;
    public string itemName;
    public string description;
    public Sprite itemIcon;
    public Dictionary<string, int> iteminfo = new Dictionary<string, int>();


    public Item(int id, string name,string desc,Dictionary<string,int> iteminfo)
    {
        this.itemId = id;
        this.itemName = name;
        this.description = desc;
        this.itemIcon = Resources.Load<Sprite>("Icons/" + name);
        this.iteminfo = iteminfo;
    }

    public Item(Item item)  //create item
    {
        this.itemId = item.itemId;
        this.itemName = item.itemName;
        this.description = item.description;
        this.itemIcon = Resources.Load<Sprite>("Icons/" + itemName);
        this.iteminfo = item.iteminfo;
    }

}
