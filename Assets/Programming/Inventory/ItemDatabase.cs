using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryPanel;
    public List<Item> items = new List<Item>();
    private bool toggled;

    // Start is called before the first frame update
     void Awake()
    {
        Build();
    }
    private void Start()
    {
        inventoryPanel.SetActive(false);
    }

    public void OpenInventory()
    {
        toggled = !toggled;
        if(toggled)
        {
            inventoryPanel.SetActive(true);
            Build();
        }
        else
        {
            inventoryPanel.SetActive(false);
        }

    }

    private void Update()
    {
        Build();
    }
    void Build()
    {
        items = new List<Item>{ new Item(0, "Robot", "this is a Robot", new Dictionary<string, int>
        {
            { "Cost", 35 },
            { "Sell", 20 }
        }),
        new Item(1,"Syringe","this is a syringe",new Dictionary<string, int>
        {
            {"Cost",50 },
            {"Durability",90 }
        }),
        new Item(2,"Medicine","Medicine used for treatment",new Dictionary<string, int>
        {
            {"Power",100 }
        })
        };
    }

    public Item FindItem(int id)
    {
        return items.Find(item => item.itemId == id);       
    }

    public Item FindItem(string name)
    {
        return items.Find(item => item.itemName == name);       
    }
}
