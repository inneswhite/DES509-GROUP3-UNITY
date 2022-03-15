using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemStats : MonoBehaviour
{
    private Text stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponentInChildren<Text>();       
    }


    public void GetStats(Item item)
    {
        string statText = "";
        if(item.iteminfo.Count>0)
        {
            foreach(var iteminfo in item.iteminfo)
            {
                statText += iteminfo.Key.ToString() + ":" + iteminfo.Value.ToString() + "\n";
            }
        }
        string itemstats = string.Format("<b>{0}</b>\n{1}\n\n<b>{2}</b>\n", item.itemName, item.description,statText);
        stats.text = itemstats;
        gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
