using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PickItem()
    {
        Inventory.instance.AddItem(item);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        PickItem();
    }

}
