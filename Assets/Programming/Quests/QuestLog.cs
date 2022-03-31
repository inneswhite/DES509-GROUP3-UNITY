using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLog : MonoBehaviour
{
    private bool isLogOpen;
    public List<Quest> quests = new List<Quest>();




    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

 


    public bool GetTaskCompleted(int no)
    {
        return quests[no].isComplete;
    }

    public void Method()
    {
        if (quests[0].CurrentStatus == Quest.QuestType.Open)
        {
            quests[0].ItemsCollected();
            if (quests[0].isReached())
            {
                quests[0].TaskCompleted();
                Debug.Log("Task has been completed");
            }
        }
    }



    public void ItemsMisplaced(int no)
    {
        if (quests[no].CurrentStatus == Quest.QuestType.Open)
        {
            quests[no].ItemsDisposed();
        }
    }




    public void Complete(int no)
    {
        if (quests[no].CurrentStatus == Quest.QuestType.Open)
        {
            quests[no].ItemsCollected();
            if (quests[no].isReached())
            {
                quests[no].TaskCompleted();
                quests[no].isComplete = true;
            }
        }
    }
}


   