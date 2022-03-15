using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{ 
    private Inventory inventory;
    public int itemId;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  

    private void OnMouseDown()
    {
        if(itemId==0)
        {
            inventory.GiveItem(0);
            this.gameObject.SetActive(false);
        }
        if (itemId == 1)
        {
            inventory.GiveItem(1);
            this.gameObject.SetActive(false);
        }
        if (itemId == 2)
        {
            inventory.GiveItem(2);
            this.gameObject.SetActive(false);
        }

    }
}
