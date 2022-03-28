using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    [SerializeField]
    private string description;

    public Text descriptionText;
    public Text currentamountText;

    public Text requiredamountText;
    public bool isComplete;

    public bool AssignedQuest { get; set; }
    public bool Helped { get; set; }


    void Awake()
    {

    }
    void Start()
    {

        //CurrentStatus = QuestType.Closed;

    }

    public enum QuestType
    {
        Open, Closed
    }
    public QuestType CurrentStatus;

    public int currentAmount;
    public int requiredAmount;

    public bool isReached()
    {
        return (currentAmount >= requiredAmount);
    }


    public void ActivateQuest()
    {
        CurrentStatus = QuestType.Open;
        AssignedQuest = true;
        if (CurrentStatus == QuestType.Open)
        {
            descriptionText.GetComponent<Text>().text = description;
            currentamountText.GetComponent<Text>().text = currentAmount.ToString();
            requiredamountText.GetComponent<Text>().text = requiredAmount.ToString();
        }
    }
    public void ItemsCollected()
    {
        if (CurrentStatus == QuestType.Open)
        {
            currentAmount += 1;
            currentamountText.GetComponent<Text>().text = currentAmount.ToString();
        }
    }


    public void ItemsDisposed()
    {
        if (CurrentStatus == QuestType.Open)
        {
            currentAmount -= 1;
            currentamountText.GetComponent<Text>().text = currentAmount.ToString();
        }
    }
    public void TaskCompleted()
    {
        CurrentStatus = QuestType.Closed;
        Helped = true;
        AssignedQuest = false;
    }
}