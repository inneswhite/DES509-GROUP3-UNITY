using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DialogueManager : MonoBehaviour
{
    public NPC npc;
    public Text npcName;
    public Text dialoguetext;
    [SerializeField]
    private bool isActive;

    public PlayerCop cop;
    public Text copName;
    public Text copdialoguetext;

    public EndDialogue End;

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
    [SerializeField]
    private GameObject choicePanel;

    [SerializeField]
    private GameObject choicePanel2;

    [SerializeField]
    private GameObject choicePanel3;



    private int Tracker;
    private int relationshipvalue;





    // Start is called before the first frame update
    void Start()
    {
        typingspeed = GetComponent<TypingSpeed>();
        Dialoguecanvas.SetActive(false);
        Copdialoguecanvas.SetActive(false);
        playercontroller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        choicePanel.SetActive(false);
        choicePanel2.SetActive(false);
        choicePanel3.SetActive(false);

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
                    StartCoroutine(ReadingDialogue(npc));      // Starting Dialogue
                    Debug.Log("i am there");
                }
                else if (sequencenumber == 1)
                {
                    StartCoroutine(ReadQuestDialogue(npc));      //quest dialogue
                    Debug.Log("I am the quest");
  
                }
                else if (sequencenumber == 2)
                {
                    if (inventory.confiscatednumber == 2)
                        {
                        StartCoroutine(FinalDecision(npc));
                        Debug.Log("i have decided");
                        }
                    else if(inventory.confiscatednumber==1)
                    {
                        StartCoroutine(SingleDecision(npc));
                        Debug.Log("i have decided about single item");
                    }
                }
            }
        }
    }



    private IEnumerator ReadingDialogue(NPC npc)
    {
        PlayCopDialogue();                              // COP START DIALOGUE
        yield return new WaitForSeconds(1f);
        foreach(string dialogue in cop.playerstartdialogue)     
        {
            yield return typingspeed.Run(dialogue, copdialoguetext);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        StopCopDialogue();
        yield return new WaitForSeconds(1f);
        PlayNPCDialogue();                              // PLAYER START NPC DIALOGUE
        foreach (string dialogue in npc.npcdialogue)
        {
            yield return typingspeed.Run(dialogue, dialoguetext);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        StopNPCDialogue();
        yield return new WaitForSeconds(1f);       
        PlayCopDialogue();
        foreach (string dialogue in cop.playerstartdialogue2)
        {
            yield return typingspeed.Run(dialogue, copdialoguetext);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        StopCopDialogue();
        yield return new WaitForSeconds(1f);
        PlayNPCDialogue();
        foreach(string dialogue in npc.npcdialogue2)
        {
            yield return typingspeed.Run(dialogue, dialoguetext);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        StopNPCDialogue();
        yield return new WaitForSeconds(1f);
        PlayCopDialogue();
        foreach (string dialogue in cop.playerstartdialogue3)
        {
            yield return typingspeed.Run(dialogue, copdialoguetext);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        yield return new WaitForSeconds(1f);
        CloseCopDialogue();
    }



    public void CloseCopDialogue()          //AFTER FIRST INTERACTION
    {
        Copdialoguecanvas.SetActive(false);
        copdialoguetext.text = null;
        sequencenumber++;
        isActive = false;
        quests[0].ActivateQuest();      //Activate your first quest
    }


    public void DuringSearch()
    {
                                               // Search Dialogue
    }



    // NPC AFTER QUEST DIALOGUE






    private IEnumerator ReadQuestDialogue(NPC npc)
    {
        yield return new WaitForSeconds(1f);
        PlayCopDialogue();
        if (inventory.confiscatednumber == 0)
        {
            foreach (string dialogue in cop.playeroptiondialogue)
            {
                yield return typingspeed.Run(dialogue, copdialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            StartCoroutine(LastConversation());         //Have Final Conversation
        }
        else if (inventory.confiscatednumber == 1)
        {
            foreach (string dialogue in cop.playeroptiondialogue2)
            {
                yield return typingspeed.Run(dialogue, copdialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            ConfiscateSingleItem();         // If Player has single item 
        }
        else if (inventory.confiscatednumber == 2)
        {
            foreach (string dialogue in cop.playeroptiondialogue2)
            {
                yield return typingspeed.Run(dialogue, copdialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            ConfiscateBothItems();          // If Player has two items
        }
        StopCopDialogue();      // Cop stops talking
    }

    /*IF COP HAS SINGLE ITEM*/
    public void ConfiscateSingleItem()
    {
        StartCoroutine(SingleItemConversation());
    }

    IEnumerator SingleItemConversation()
    {
        isActive = true;
        yield return new WaitForSeconds(1f);
        if (inventory.inventoryid == 0 && inventory.confiscatednumber == 1)         //ROBOT
        {
            foreach (string dialogue in npc.choicedialogue)     // DISPLAY ROBOT DIALOGUE
            {
                yield return typingspeed.Run(dialogue, dialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            StopNPCDialogue();
            yield return new WaitForSeconds(1f);
            choicePanel2.SetActive(true);
        }
        else if (inventory.inventoryid == 1 && inventory.confiscatednumber == 1)     //SYRINGE
        {
            PlayNPCDialogue();
            foreach (string dialogue in npc.choicedialogue)         //DISPLAY SYRINGE DIALOGUE 
            {
                yield return typingspeed.Run(dialogue, dialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            StopNPCDialogue();
            yield return new WaitForSeconds(1f);
            choicePanel3.SetActive(true);
        }
    }

    public void RobotChoice()
    {
        relationshipvalue = -2;
        choicePanel2.SetActive(false);   
        sequencenumber++;
        isActive = false;
        Debug.Log("chose first option");
    }

    public void RobotChoice2()
    {
        relationshipvalue = 1;
        choicePanel2.SetActive(false);
        sequencenumber++;
        isActive = false;
        Debug.Log("chose second option");
    }

    public void SyringeChoice()
    {
        relationshipvalue = 1;
        choicePanel3.SetActive(false);
        sequencenumber++;
        isActive = false;
        Debug.Log("chose first option");
    }

    public void SyringeChoice2()
    {
        relationshipvalue = -1;
        choicePanel3.SetActive(false);
        sequencenumber++;
        isActive = false;
        Debug.Log("chose second option");
    }

    IEnumerator SingleDecision(NPC npc)
    {
        yield return new WaitForSeconds(2f);
        if(relationshipvalue==1)                    //GIVE AWAY SINGLE ITEM
        {
            PlayCopDialogue();
            foreach (string dialogue in cop.bothitemsdialogue)
            {
                yield return typingspeed.Run(dialogue, copdialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            StopCopDialogue();
            yield return new WaitForSeconds(2f);
            PlayNPCDialogue();
            foreach (string dialogue in npc.finaldialogue)
            {
                yield return typingspeed.Run(dialogue, dialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            StopNPCDialogue();
            StartCoroutine(LastConversation());
        }
        if (relationshipvalue == -2)    //TAKE AWAY ROBOT
        {
            PlayCopDialogue();
            foreach (string dialogue in cop.singleitemdialogue)
            {
                yield return typingspeed.Run(dialogue, copdialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            StopCopDialogue();
            yield return new WaitForSeconds(2f);
            PlayNPCDialogue();
            foreach (string dialogue in npc.finaldialogue3)
            {
                yield return typingspeed.Run(dialogue, dialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            StopNPCDialogue();
            sequencenumber++;
        }
        if (relationshipvalue == -1)    //TAKE AWAY SYRINGE     
        {
            PlayCopDialogue();
            foreach (string dialogue in cop.singleitemdialogue2)
            {
                yield return typingspeed.Run(dialogue, copdialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            StopCopDialogue();
            yield return new WaitForSeconds(2f);
            PlayNPCDialogue();
            foreach (string dialogue in npc.finaldialogue2)
            {
                yield return typingspeed.Run(dialogue, dialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            StopNPCDialogue();
            sequencenumber++;
        }
    }



    /* IF COP HAS TWO ITEMS*/
    public void ConfiscateBothItems()               
    {
        Copdialoguecanvas.SetActive(false);
        copdialoguetext.text = null;
        quests[0].TaskCompleted();
        StartCoroutine(MakeChoiceDialogue(npc));
    }

  
    /* COP MAKES A CHOICE */
    private IEnumerator MakeChoiceDialogue(NPC npc)
    {
        yield return new WaitForSeconds(2f);
        PlayNPCDialogue();          // npc responds
        foreach (string dialogue in npc.choicedialogue)
        {
            yield return typingspeed.Run(dialogue, dialoguetext);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        StopNPCDialogue();
        yield return new WaitForSeconds(2f);
        choicePanel.SetActive(true);            //Make a choice
    }


    public void PlayerChoice()
    {
        Choice();
    }

    public void PlayerChoice2()
    {
        Choice2();
    }

    public void PlayerChoice3()
    {
        Choice3();
    }

    public void PlayerChoice4()
    {
        Choice4();
    }
    protected bool Choice()
    {
        isActive = false;
        choicePanel.SetActive(false);
        relationshipvalue = 2;
        sequencenumber++;
        isActive = false;
        Debug.Log("chose 1");
        return true;
    }

    protected bool Choice2()
    {
        isActive = false;
        choicePanel.SetActive(false);
        relationshipvalue = -2;
        sequencenumber++;
        isActive = false;
        Debug.Log("chose 2");
        return true;
    }

    protected bool Choice3()
    {
        isActive = false;
        choicePanel.SetActive(false);
        relationshipvalue = -2;
        sequencenumber++;
        isActive = false;
        Debug.Log("chose 3");
        return true;
    }

    protected bool Choice4()
    {
        isActive = false;
        choicePanel.SetActive(false);
        relationshipvalue = -5;
        sequencenumber++;
        isActive = false;
        Debug.Log("chose 4");
        return true;
    }

    private IEnumerator FinalDecision(NPC npc)
    {
        if (relationshipvalue == 2)         //RETURN BOTH THE ITEMS
        {
            PlayCopDialogue();
            foreach (string dialogue in cop.bothitemsdialogue)
            {
                yield return typingspeed.Run(dialogue, copdialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            StopCopDialogue();
            yield return new WaitForSeconds(2f);
            PlayNPCDialogue();
            foreach (string dialogue in npc.finaldialogue)
            {
                yield return typingspeed.Run(dialogue, dialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            StopNPCDialogue();
            StartCoroutine(LastConversation());
        }

        if (relationshipvalue == -2)                 //KEEP TOY BUT TAKE AWAY SYRINGE
        {
            PlayCopDialogue();
            foreach (string dialogue in cop.bothitemsdialogue2)
            {
                yield return typingspeed.Run(dialogue, copdialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            StopCopDialogue();
            yield return new WaitForSeconds(2f);
            PlayNPCDialogue();
            foreach (string dialogue in npc.finaldialogue2)
            {
                yield return typingspeed.Run(dialogue, dialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            }
            StopNPCDialogue();
            StartCoroutine(LastConversation());
        }
        if (relationshipvalue == -3)                 //KEEP SYRINGE BUT TAKE AWAY ROBOT
        {
            PlayCopDialogue();
            foreach (string dialogue in cop.bothitemsdialogue3)
            {
                yield return typingspeed.Run(dialogue, copdialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            StopCopDialogue();
            yield return new WaitForSeconds(2f);
            PlayNPCDialogue();
            foreach (string dialogue in npc.finaldialogue3)
            {
                yield return typingspeed.Run(dialogue, dialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            }
            StopNPCDialogue();
            StartCoroutine(LastConversation());
        }
        if (relationshipvalue == -5)                 //TAKE AWAY BOTH ITEMS
        {
            PlayCopDialogue();
            foreach (string dialogue in cop.bothitemsdialogue4)
            {
                yield return typingspeed.Run(dialogue, copdialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            StopCopDialogue();
            yield return new WaitForSeconds(2f);
            PlayNPCDialogue();
            foreach (string dialogue in npc.finaldialogue4)
            {
                yield return typingspeed.Run(dialogue, dialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            StopNPCDialogue();
            StartCoroutine(LastConversation());
        }
    }


    IEnumerator LastConversation()      //HAVE LAST CONVERSATION
    {
        //End
        yield return new WaitForSeconds(1f);
        PlayNPCDialogue();
        foreach(string dialogue in End.endnpcdialogue)
        {
            yield return typingspeed.Run(dialogue, dialoguetext);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        StopNPCDialogue();
        yield return new WaitForSeconds(2f);
        PlayCopDialogue();
        foreach(string dialogue in End.endcopdialogue)
        {
            yield return typingspeed.Run(dialogue, copdialoguetext);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        StopCopDialogue();
        yield return new WaitForSeconds(2f);
        PlayNPCDialogue();
        foreach (string dialogue in End.endnpcdialogue2)
        {
            yield return typingspeed.Run(dialogue, dialoguetext);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        StopNPCDialogue();
        yield return new WaitForSeconds(2f);
        PlayCopDialogue();
        foreach (string dialogue in End.endcopdialogue2)
        {
            yield return typingspeed.Run(dialogue, copdialoguetext);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        StopCopDialogue();
        yield return new WaitForSeconds(2f);
        PlayNPCDialogue();
        foreach (string dialogue in End.endnpcdialogue3)
        {
            yield return typingspeed.Run(dialogue, dialoguetext);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        StopNPCDialogue();
        yield return new WaitForSeconds(2f);
        PlayCopDialogue();
        foreach (string dialogue in End.endcopdialogue3)
        {
            yield return typingspeed.Run(dialogue, copdialoguetext);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        StopCopDialogue();
        yield return new WaitForSeconds(1f);
        PlayNPCDialogue();
        foreach(string dialogue in End.endnpcdialogue4)
        {
            yield return typingspeed.Run(dialogue, dialoguetext);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        StopNPCDialogue();
        yield return new WaitForSeconds(1f);   
        sequencenumber++;
    }



    public void PlayNPCDialogue()
    {
        isActive = true;
        Dialoguecanvas.SetActive(true);
        npcName.text = npc.NPCName;
    }

    public void StopNPCDialogue()
    {
        isActive = false;
        Dialoguecanvas.SetActive(false);
        npcName.text = npc.NPCName;
        dialoguetext.text = null;
    }

    public void PlayCopDialogue()
    {
        isActive = true;
        Copdialoguecanvas.SetActive(true);
        copName.text = cop.copName;       
    }

    public void StopCopDialogue()
    {
        isActive = false;
        Copdialoguecanvas.SetActive(false);
        copName.text = cop.copName;
        copdialoguetext.text = null;
    }



    void Update()               
    {
        Talking();
        if (isActive)
        {
            playercontroller.istalking = true;
        }
        else if (!isActive)
        {
            playercontroller.istalking = false;
        }
    }
}
    
 
