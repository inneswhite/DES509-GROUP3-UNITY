using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public bool AssignedQuest { get; set; }
    public bool Helped { get; set; }

    public NPC npc;

    [SerializeField]
    private GameObject quests;

 
    private Quest task { get; set; }
    public void Interact()
    {

        if (!AssignedQuest && !Helped)
        {
            Debug.Log("i am interacting");
            AssignQuest();
        }
        else if (AssignedQuest && !Helped)
        {
            CheckQuest();
        }
        else
        {
            // DialogueSystem.Instance.AddNewDialogue(new string[] { "Thanks for that stuff that one time." }, name);
        }
    }

    void AssignQuest()
    {
        AssignedQuest = true;
 
    }

    void CheckQuest()
    {
            Helped = true;
            AssignedQuest = false;
        }
    }
