using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler,IDropHandler
{
    public Item item;
    private Image itemIcon;
    private UIItem selecteditem;
    private Inventory inventory;
    private ItemStats itemstats;                // search item stats 
    private GameObject statsPanel;
    private SpawnItem spawnitem;
    // Start is called before the first frame update
    void Start()
    {
        itemIcon = GetComponent<Image>();
        UpdateUI(null);
        selecteditem = GameObject.FindGameObjectWithTag("SelectedIcon").GetComponent<UIItem>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
  //      itemstats = GameObject.FindGameObjectWithTag("ItemStats").GetComponent<ItemStats>();  
        spawnitem = GameObject.FindGameObjectWithTag("SelectedIcon").GetComponent<SpawnItem>();

    }

  

    public void UpdateUI(Item item)
    {
        this.item = item;           // get current item
        if(this.item!=null)     // change color     
        {
            itemIcon.color = Color.white;
            itemIcon.sprite = this.item.itemIcon;
        }
        else     // make icon transparent
        {
            itemIcon.color = Color.clear;
        }
    }

    public void RemoveUI(Item item)
    {
        this.item = item; // remove item 
        itemIcon.color = Color.clear;       // make icon transparent
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(this.item != null)
        {
            if(selecteditem.item!=null)
            {
                Item clone = new Item(selecteditem.item);           // item is selected
                selecteditem.UpdateUI(this.item);
               
                UpdateUI(clone);
            }
            else
            {
                selecteditem.UpdateUI(this.item);           // item isnt selected
                UpdateUI(null);
            }
        }
        else if(selecteditem.item!=null)
        {
            UpdateUI(selecteditem.item);                
            selecteditem.UpdateUI(null);
        }
    }


    void Update()
    {
        if (selecteditem.item != null)
        {
            if (Input.GetMouseButtonDown(0))
            {

                Debug.Log("asa");
                if (!EventSystem.current.IsPointerOverGameObject())             //is pointer not over UI 
                {
                    if (selecteditem.item.itemId == 0)
                    {
                        Debug.Log("I am spawned");
                        spawnitem.SpawningItem();
                        inventory.RemoveItem(selecteditem.item.itemId);
                        selecteditem.UpdateUI(null);
                    }
                    else if (selecteditem.item.itemId == 1)
                    {
                        spawnitem.SpawningItem2();
                        inventory.RemoveItem(selecteditem.item.itemId);
                        selecteditem.UpdateUI(null);
                    }
                    else if (selecteditem.item.itemId == 2)
                    {
                        spawnitem.SpawningItem3();
                        inventory.RemoveItem(selecteditem.item.itemId);
                        selecteditem.UpdateUI(null);
                    }
                }
            }
        }
    }

   


    public void OnDrop(PointerEventData eventData)
    {

           
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      if(this.item!=null)
        {
   //         itemstats.GetStats(this.item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
  //      itemstats.gameObject.SetActive(false);
    }
}
