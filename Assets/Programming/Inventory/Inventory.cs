using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> inventoryitems = new List<Item>();
    public ItemDatabase itemdatabase;
    public DisplayInventory displayinventory;


    private void Start()
    {

        //RemoveItem(1);
    }
    public void GiveItem(int id)
    {
        Item items = itemdatabase.FindItem(id);
        inventoryitems.Add(items);
        displayinventory.AddItemSlot(items);
        Debug.Log("Added item:" + items.itemName);
    }

    public void GiveItem(string itemname)
    {
        Item items = itemdatabase.FindItem(itemname);
        inventoryitems.Add(items);
        displayinventory.AddItemSlot(items);
        Debug.Log("Added item:" + items.itemName);
    }

    public Item CheckItem(int id)
    {
        return inventoryitems.Find(item => item.itemId == id);
    }

    public void RemoveItem(int id)
    {
        Item item = CheckItem(id);     //check if item is in inventory
        if(item!=null)
        {
            inventoryitems.Remove(item);
            displayinventory.RemoveItemSlot(item);
            Debug.Log("item removed:" + item.itemName);
        }
    }
}
