using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveItem : MonoBehaviour
{
    Item item;

    public Button remove;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Remove()
    {
        Inventory.instance.RemoveItem(item);

        Destroy(gameObject);
    }

    public void Additem(Item thisitem)
    {
        item = thisitem;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
