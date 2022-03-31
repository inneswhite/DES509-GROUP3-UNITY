using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public NPC npc;
    public Text npcName;
    public Text dialoguetext;
    public bool isActive;

    public PlayerCop cop;
    public Text copName;
    public Text copdialoguetext;

    [SerializeField]
    private float dist;

    public GameObject player;
    public GameObject Dialoguecanvas;
    public GameObject Copdialoguecanvas;

    private TypingSpeed typingspeed;
    private PlayerController playercontroller;
    private Inventory inventory;
    [Header("List of quests")]
    [SerializeField]
    private Quest[] quests;
    [SerializeField]
    private int sequencenumber;
    


    // Start is called before the first frame update
    void Start()
    {
        typingspeed = GetComponent<TypingSpeed>();
        Dialoguecanvas.SetActive(false);
        Copdialoguecanvas.SetActive(false);
        playercontroller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }



    public void Talking()
    {
        dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < 4f)
        {
            if (Input.GetKeyDown(KeyCode.E) && isActive == false)
            {
                if (sequencenumber == 0)
                {
           
                   ShowNPCdialogue(npc);       // Starting Dialogue
                    npcName.text = npc.name;
                    Debug.Log("i am there");
                }
                else if(sequencenumber==1)
                {
                    ShowNPCdialoguequest(npc);      //quest dialogue
                    npcName.text = npc.name;
                    Debug.Log("I am the quest");
                }
            }
        }
    }

    public void ShowNPCdialogue(NPC npc)
    {
        StartCoroutine(ReadingDialogue(npc));
        isActive = true;

        Dialoguecanvas.SetActive(true);         //set npc dialogue panel
        Copdialoguecanvas.SetActive(false);

        cop.currentState = PlayerCop.playerState.Talking;
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

    private IEnumerator ReadPlayerDialogue(PlayerCop cop)
    {
        yield return new WaitForSeconds(3f);
        Copdialoguecanvas.SetActive(true);
        copName.text = cop.copName;
        foreach (string dialogue in cop.playerstartdialogue)
        {
            yield return typingspeed.Run(dialogue, copdialoguetext);

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        CloseCopDialogue();
    }

    public void CloseDialogue()
    {
        Dialoguecanvas.SetActive(false);
        dialoguetext.text = null;
        StartCoroutine(ReadPlayerDialogue(cop));        //start player cop dialogue
    }

    public void CloseCopDialogue()
    {
        Copdialoguecanvas.SetActive(false);
        copdialoguetext.text = null;
        cop.currentState = PlayerCop.playerState.idle;
        sequencenumber++;
        isActive = false;
        quests[0].ActivateQuest();      //Activate your first quest
    }



    // NPC AFTER QUEST DIALOGUE

    public void ShowNPCdialoguequest(NPC npc)
    {
        StartCoroutine(ReadingQuestDialogue(npc));
        isActive = true;

        Dialoguecanvas.SetActive(true);         //set npc dialogue after quest panel
        Copdialoguecanvas.SetActive(false);

        cop.currentState = PlayerCop.playerState.Talking;

    }


    private IEnumerator ReadingQuestDialogue(NPC npc)
    {
        yield return new WaitForSeconds(2f);
        foreach (string dialogue in npc.questdialogue)
        {
            yield return typingspeed.Run(dialogue, dialoguetext);

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        Dialoguecanvas.SetActive(false);
        StartCoroutine(ReadPlayerQuestDialogue(cop));
    }

    private IEnumerator ReadPlayerQuestDialogue(PlayerCop cop)
    {
        yield return new WaitForSeconds(3f);
        Copdialoguecanvas.SetActive(true);
        copName.text = cop.copName;
        if (inventory.confiscatednumber == 0)
        {
            foreach (string dialogue in cop.playeroptiondialogue)
            {
                yield return typingspeed.Run(dialogue, copdialoguetext);

                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
        }
        else if(inventory.confiscatednumber==1)
        {
            foreach (string dialogue in cop.playeroptiondialogue2)
            {
                yield return typingspeed.Run(dialogue, copdialoguetext);

                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
        }
        else if (inventory.confiscatednumber == 2)
        {
            foreach (string dialogue in cop.playeroptiondialogue3)
            {
                yield return typingspeed.Run(dialogue, copdialoguetext);

                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
        }
        Copdialoguecanvas.SetActive(false);
        sequencenumber++;
        isActive = false;
        copdialoguetext.text = null;
        cop.currentState = PlayerCop.playerState.idle;
    }



    void Update()
    {  
            Talking();   
    }
}
    
 
