using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<Item> items = new List<Item>();

    public Transform container;
    public GameObject inventoryitem;
    public RemoveItem[] removeitems;

    private void Awake()
    {

        instance = this;

        if(instance!=this)
        {
            DontDestroyOnLoad(gameObject);
        }    
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    public void ListItems()
    {
        foreach(Transform item in container)
        {
            Destroy(item.gameObject);
        }
        foreach(var item in items)
        {
            GameObject obj = Instantiate(inventoryitem, container);
            var itemIcon = obj.transform.Find("itemIcon").GetComponent<Image>();           
            itemIcon.sprite = item.Icon;
            var removebutton = obj.transform.Find("Close Button").GetComponent<Button>();
        }

        SetItems();
    }

   public void SetItems()
    {
        removeitems = container.GetComponentsInChildren<RemoveItem>();

        for(int i=0;i<items.Count;i++)
        {
            removeitems[i].Additem(items[i]);
        }
    }
}
