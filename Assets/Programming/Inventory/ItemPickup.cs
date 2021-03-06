using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    private Inventory inventory;
    public int itemId;
    private PlayerController player;
    private float distance;
    private DialogueManager dm;
    private InspectManager inspectmanager;
    public AK.Wwise.Event Sound;
    private GameObject medicine;
    private GameObject medicineParent;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        dm = GameObject.FindGameObjectWithTag("NPC").GetComponent<DialogueManager>();
        inspectmanager = GameObject.FindGameObjectWithTag("InspectManager").GetComponent<InspectManager>();
        medicine = GameObject.Find("Medicine");
        medicineParent = GameObject.Find("MedicineParent");
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, this.transform.position);
    }



    private void OnMouseDown()
    {
        if (dm.currentstate == DialogueManager.State.investigate)
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
                        medicineParent.GetComponent<BoxCollider>().enabled = false;
                        medicine.SetActive(false);
                    }
                }
            }
        }
        else
        {
            Debug.Log("Error player is not investigating yet");
        }
    }
}
