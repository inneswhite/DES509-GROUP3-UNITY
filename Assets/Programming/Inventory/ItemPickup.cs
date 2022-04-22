using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{ 
    private Inventory inventory;
    public int itemId;
    private PlayerController player;
    private float distance;
    private InspectManager inspectmanager;
    public AK.Wwise.Event Sound;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        inspectmanager = GameObject.FindGameObjectWithTag("InspectManager").GetComponent<InspectManager>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, this.transform.position);
    }



    private void OnMouseDown()
    {
        if (distance < 3f)
        {
            if (inspectmanager.isInspect == false)
            {

                Sound.Post(gameObject);
                if (itemId == 0)
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
    }
}
