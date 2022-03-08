using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public NPC npc;
    public Text npcName;
    public Text dialoguetext;
    public bool isActive;

    [SerializeField]
    private float dist;
    private float dialoguenumber;

    public GameObject player;
    public GameObject Dialoguecanvas;

    private TypingSpeed typingspeed;
    private PlayerController playercontroller;

    // Start is called before the first frame update
    void Start()
    {
        typingspeed = GetComponent<TypingSpeed>();
        Dialoguecanvas.SetActive(false);
        playercontroller = GameObject.Find("Player").GetComponent<PlayerController>();

    }

   

    public void Talking()
    {
        dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < 4f)
        {
            if (Input.GetKeyDown(KeyCode.E) && isActive == false)
            {
                Showdialogue(npc);
                npcName.text = npc.name;
                Debug.Log("i am there");
            }
        }
    }

    public void Showdialogue(NPC npc)
    {
        StartCoroutine(ReadingDialogue(npc));
        Dialoguecanvas.SetActive(true);
        playercontroller.isOpen = true;
    }

    private IEnumerator ReadingDialogue(NPC npc)
    {
        yield return new WaitForSeconds(2f);
        foreach (string dialogue in npc.npcdialogue)
        {
            yield return typingspeed.Run(dialogue, dialoguetext);
            
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        CloseDialogue();
    }

    void Update()
    {
        Talking();
    }


    public void CloseDialogue()
    {
        Dialoguecanvas.SetActive(false);
        playercontroller.isOpen = false;
    }
}
    
 
