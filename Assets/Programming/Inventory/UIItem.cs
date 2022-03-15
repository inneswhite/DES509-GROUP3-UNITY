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
    private ItemStats itemstats;
    private GameObject statsPanel;
    // Start is called before the first frame update
    void Awake()
    {
        itemIcon = GetComponent<Image>();
        UpdateUI(null);
        selecteditem = GameObject.Find("SelectedIcon").GetComponent<UIItem>();
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        itemstats = GameObject.Find("ItemStats").GetComponent<ItemStats>();
    }

  

    public void UpdateUI(Item item)
    {
        this.item = item;
        if(this.item!=null)
        {
            itemIcon.color = Color.white;
            itemIcon.sprite = this.item.itemIcon;
        }
        else
        {
            itemIcon.color = Color.clear;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(this.item != null)
        {
            if(selecteditem.item!=null)
            {
                Item clone = new Item(selecteditem.item);
                selecteditem.UpdateUI(this.item);
               
                UpdateUI(clone);
            }
            else
            {
                selecteditem.UpdateUI(this.item);
                UpdateUI(null);
            }
        }
        else if(selecteditem.item!=null)
        {
            UpdateUI(selecteditem.item);
            selecteditem.UpdateUI(null);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(selecteditem.item!=null)
        {
           
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      if(this.item!=null)
        {
            itemstats.GetStats(this.item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemstats.gameObject.SetActive(false);
    }
}
