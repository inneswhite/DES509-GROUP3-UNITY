using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quest : MonoBehaviour
{
    [SerializeField]
    private string description;

    public TextMeshProUGUI descriptionText;

    public bool isComplete;

    public bool AssignedQuest { get; set; }
    public bool Helped { get; set; }


    void Awake()
    {

    }
    void Start()
    {
        //descriptionText = GetComponent<TextMeshProUGUI>();
        //CurrentStatus = QuestType.Closed;

    }

    public enum QuestType
    {
        Open, Closed
    }
    public QuestType CurrentStatus;






    public void ActivateQuest()
    {
        CurrentStatus = QuestType.Open;
        AssignedQuest = true;
        if (CurrentStatus == QuestType.Open)
        {
            descriptionText.GetComponent<TextMeshProUGUI>().text = description;

        }
    }



  
    public void TaskCompleted()
    {
        CurrentStatus = QuestType.Closed;
        Helped = true;
        AssignedQuest = false;
    }
}