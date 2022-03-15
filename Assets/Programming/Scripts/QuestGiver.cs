using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour {
    public bool AssignedQuest { get; set; }
    public bool Helped { get; set; }

    public NPC npc;

    [SerializeField]
    private GameObject quests;

    [SerializeField]
    private string questType;
    private Task task{ get; set; }
    public  void Interact()
    {
        
        if (!AssignedQuest && !Helped)
        {
            Debug.Log("i am interacting");
            AssignQuest();
        }
        else if(AssignedQuest && !Helped)
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
        task= (Task)quests.AddComponent(System.Type.GetType(questType));
    }

    void CheckQuest()
    {
        if (task.completed)
        {
            task.GiveReward();
            Helped = true;
            AssignedQuest = false;
           // DialogueSystem.Instance.AddNewDialogue(new string[] {"Thanks for that! Here's your reward.", "More dialogue"}, name);
        }
        else
        {
           // DialogueSystem.Instance.AddNewDialogue(new string[] { "You're still in the middle of helping me. Get back at it!"}, name);
        }
    }
}
