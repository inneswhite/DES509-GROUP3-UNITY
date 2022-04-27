using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class DialogueManager : MonoBehaviour
{
    public NPC npc;
    public TextMeshProUGUI npcName;
    public TextMeshProUGUI dialoguetext;
    [SerializeField]
    private bool isActive;

    public PlayerCop cop;
    public TextMeshProUGUI copName;
    public TextMeshProUGUI copdialoguetext;

    public EndDialogue End;

    [SerializeField]
    private float dist;

    public GameObject player;
    public GameObject Dialoguecanvas;
    public GameObject Copdialoguecanvas;

    private TypingSpeed typingspeed;
    private PlayerController playercontroller;
    private Inventory inventory;
    private NPCInventory npcinventory;
    [Header("List of quests")]
   
    public Quest[] quests;
    [SerializeField]
    private int sequencenumber;
    [SerializeField]
    private GameObject choicePanel;

    [SerializeField]
    private GameObject choicePanel2;

    [SerializeField]
    private GameObject choicePanel3;

    [Header("Camera Switch")]
    [SerializeField]
    private GameObject dialoguecamera,sideCamera2;




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
        npcinventory = GameObject.FindGameObjectWithTag("NPC").GetComponent<NPCInventory>();
        choicePanel.SetActive(false);
        choicePanel2.SetActive(false);
        choicePanel3.SetActive(false);
        quests[0].ActivateQuest();          // Activate your quest
        dialoguecamera.SetActive(false);
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
            }
        }
    }



    private IEnumerator ReadingDialogue(NPC npc)
    {
        dialoguecamera.SetActive(true);         //DIALOGUE CAM
        sideCamera2.SetActive(false);
        PlayCopDialogue();                              // COP START DIALOGUE
        yield return new WaitForSeconds(1f);
        foreach(string dialogue in cop.playerstartdialogue)     
        {
            //TEST DIALOGUE AUDIO
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
        isActive = false;
        Copdialoguecanvas.SetActive(false);
        dialoguecamera.SetActive(false);        //Turn Camera Off
        sideCamera2.SetActive(true);
        copdialoguetext.text = null;
        sequencenumber++;
        isActive = false;
    }


    public void DuringSearch()
    {
                                               // Search Dialogue
    }



    // NPC AFTER QUEST DIALOGUE






    private IEnumerator ReadQuestDialogue(NPC npc)
    {
        dialoguecamera.SetActive(true);         //Dialogue CAM
        sideCamera2.SetActive(false);
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
        StopCopDialogue();               // Cop stops talking
    }

    /*IF COP HAS SINGLE ITEM*/
    public void ConfiscateSingleItem()
    {
        StartCoroutine(SingleItemConversation());
    }

    IEnumerator SingleItemConversation()
    {
        dialoguecamera.SetActive(true);
        sideCamera2.SetActive(false);
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
        StartCoroutine(SingleDecision(npc));
        isActive = false;
        Debug.Log("chose first option");
    }

    public void RobotChoice2()
    {
        relationshipvalue = 1;
        choicePanel2.SetActive(false);
        StartCoroutine(SingleDecision(npc));
        isActive = false;
        Debug.Log("chose second option");
    }

    public void SyringeChoice()
    {
        relationshipvalue = 1;
        choicePanel3.SetActive(false);
        StartCoroutine(SingleDecision(npc));
        isActive = false;
        Debug.Log("chose first option");
    }

    public void SyringeChoice2()
    {
        relationshipvalue = -1;
        choicePanel3.SetActive(false);
        StartCoroutine(SingleDecision(npc));
        isActive = false;
        Debug.Log("chose second option");
    }

    IEnumerator SingleDecision(NPC npc)
    {
        dialoguecamera.SetActive(true);
        sideCamera2.SetActive(false);
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
            foreach (string dialogue in npc.thanksdialogue)
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
            foreach (string dialogue in cop.takerobotdialogue)
            {
                yield return typingspeed.Run(dialogue, copdialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            StopCopDialogue();
            yield return new WaitForSeconds(2f);
            PlayNPCDialogue();
            foreach (string dialogue in npc.takerobotdialogue)
            {
                yield return typingspeed.Run(dialogue, dialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            StopNPCDialogue();
            sequencenumber++;
            dialoguecamera.SetActive(false);
            sideCamera2.SetActive(true);
        }
        if (relationshipvalue == -1)    //TAKE AWAY SYRINGE     
        {
            PlayCopDialogue();
            foreach (string dialogue in cop.takesyringedialogue)
            {
                yield return typingspeed.Run(dialogue, copdialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            StopCopDialogue();
            yield return new WaitForSeconds(2f);
            PlayNPCDialogue();
            foreach (string dialogue in npc.takesyringedialogue)
            {
                yield return typingspeed.Run(dialogue, dialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            StopNPCDialogue();
            sequencenumber++;
            dialoguecamera.SetActive(false);
            sideCamera2.SetActive(true);
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
        dialoguecamera.SetActive(true);
        sideCamera2.SetActive(false);
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
        StartCoroutine(FinalDecision(npc));
        isActive = false;
        Debug.Log("chose 1");
        return true;
    }

    protected bool Choice2()
    {
        isActive = false;
        choicePanel.SetActive(false);
        relationshipvalue = -2;
        StartCoroutine(FinalDecision(npc));
        isActive = false;
        Debug.Log("chose 2");
        return true;
    }

    protected bool Choice3()
    {
        isActive = false;
        choicePanel.SetActive(false);
        relationshipvalue = -3;
        StartCoroutine(FinalDecision(npc));
        isActive = false;
        Debug.Log("chose 3");
        return true;
    }

    protected bool Choice4()
    {
        isActive = false;
        choicePanel.SetActive(false);
        relationshipvalue = -5;
        StartCoroutine(FinalDecision(npc));
        isActive = false;
        Debug.Log("chose 4");
        return true;
    }

    private IEnumerator FinalDecision(NPC npc)
    {
        dialoguecamera.SetActive(true);
        sideCamera2.SetActive(false);
        yield return new WaitForSeconds(1f);
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
            foreach (string dialogue in npc.thanksdialogue)
            {
                yield return typingspeed.Run(dialogue, dialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            inventory.RemoveItem(0);            // remove items from player inventory
            inventory.RemoveItem(1);
            npcinventory.GetItem(0);
            npcinventory.GetItem(1);
            StopNPCDialogue();
            StartCoroutine(LastConversation());
        }

        if (relationshipvalue == -2)                 //GIVE TOY BUT TAKE AWAY SYRINGE
        {
            PlayCopDialogue();
            foreach (string dialogue in cop.giverobotdialogue)
            {
                yield return typingspeed.Run(dialogue, copdialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            StopCopDialogue();
            yield return new WaitForSeconds(2f);
            PlayNPCDialogue();
            foreach (string dialogue in npc.thanksdialogue)
            {
                yield return typingspeed.Run(dialogue, dialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            }
  //          inventory.RemoveItem(0);                    // remove item from player inventory
  //          npcinventory.GetItem(0);
            StopNPCDialogue();
            StartCoroutine(LastConversation());
        }
        if (relationshipvalue == -3)                 //KEEP SYRINGE BUT TAKE AWAY ROBOT
        {
            PlayCopDialogue();
            foreach (string dialogue in cop.givesyringedialogue)
            {
                yield return typingspeed.Run(dialogue, copdialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            StopCopDialogue();
            yield return new WaitForSeconds(2f);
            PlayNPCDialogue();
            foreach (string dialogue in npc.thanksdialogue)
            {
                yield return typingspeed.Run(dialogue, dialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            }
            inventory.RemoveItem(1);                // remove item from player inventory
            npcinventory.GetItem(1);
            StopNPCDialogue();
            StartCoroutine(LastConversation());
        }
        if (relationshipvalue == -5)                 //TAKE AWAY BOTH ITEMS
        {
            PlayCopDialogue();
            foreach (string dialogue in cop.takebothdialogue)
            {
                yield return typingspeed.Run(dialogue, copdialoguetext);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            StopCopDialogue();
            yield return new WaitForSeconds(2f);
            PlayNPCDialogue();
            foreach (string dialogue in npc.takebothdialogue)
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
        dialoguecamera.SetActive(true);
        sideCamera2.SetActive(false);
        yield return new WaitForSeconds(2f);
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
        isActive = false;
        dialoguecamera.SetActive(false);
        sideCamera2.SetActive(true);
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
    
 
