using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLog : MonoBehaviour
{
    private bool isLogOpen;

    [SerializeField]
    private GameObject ObjectivePanel;


    // Start is called before the first frame update
    void Start()
    {
        ObjectivePanel.SetActive(false);
    }


    public void OpenAndCloseLog()
    {
        isLogOpen = !isLogOpen;

        if (isLogOpen)
        {
      
            ObjectivePanel.SetActive(true);
        }
        else
        {
            ObjectivePanel.SetActive(false);
        }
    }
}








   