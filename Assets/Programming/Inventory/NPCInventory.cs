using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInventory : MonoBehaviour
{
    public List<Item> inventoryitems = new List<Item>();
    public ItemDatabase itemdatabase;
    public int confiscatednumber;
    private Item items;


    private void Start()
    {


    }



    public void GetItem(int id)
    {
  
        items = itemdatabase.FindItem(id);
        inventoryitems.Add(items);
        Debug.Log("Added item:" + items.itemName);
    }


    public Item CheckItem(int id)
    {
        return inventoryitems.Find(item => item.itemId == id);

    }
}
