using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    public List<UIItem> uiItems = new List<UIItem>();
    public GameObject inventoryslot;
    public Transform slottransform;
    public int numberofslots = 16;
    // Start is called before the first frame update
    void Awake()
    {
        for(int i=0;i<numberofslots;i++)
        {
            if (i < 16)
            {
                GameObject instance = Instantiate(inventoryslot);
                instance.transform.SetParent(slottransform);
                uiItems.Add(instance.GetComponentInChildren<UIItem>());
            }
        }
        
    }

    public void UpdateSlot(int slot,Item item)
    {
        uiItems[slot].UpdateUI(item);
    }

    public void AddItemSlot(Item item)
    {
        UpdateSlot(uiItems.FindIndex(i => i.item == null), item);
    }

    public void RemoveItemSlot(Item item)
    {
        UpdateSlot(uiItems.FindIndex(i => i.item == item), null);
    }


    // Update is called once per frame
    void Update()
    {
     
    }
}
