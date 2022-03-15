using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{ 
    private Inventory inventory;
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
        inventory.RemoveItem(0);
    }
}
