using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    private UIItem selecteditem;
    private Inventory inventory;
    public GameObject[] itemprefabs;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private bool istoggled;
   
    // Start is called before the first frame update
    void Start()
    {
        selecteditem= GameObject.Find("SelectedIcon").GetComponent<UIItem>();
        inventory = GameObject.Find("Player").GetComponent<Inventory>();

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SpawningItem()
    {
        istoggled = !istoggled;
        if (istoggled)
        {
            Vector3 playerPosition = new Vector3(player.position.x+2, player.position.y+2, player.position.z+2);
            Instantiate(itemprefabs[0], playerPosition, Quaternion.identity);
        }
    }

    public void SpawningItem2()
    {
        istoggled = !istoggled;
        if (istoggled)
        {
            Vector3 playerPosition = new Vector3(player.position.x + 2, player.position.y + 2, player.position.z + 2);
            Instantiate(itemprefabs[1], playerPosition, Quaternion.identity);
        }
    }

    public void SpawningItem3()
    {
        istoggled = !istoggled;
        if (istoggled)
        {
            Vector3 playerPosition = new Vector3(player.position.x + 2, player.position.y + 2, player.position.z + 2);
            Instantiate(itemprefabs[2], playerPosition, Quaternion.identity);
        }
    }
}