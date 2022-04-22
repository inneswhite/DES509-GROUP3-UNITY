using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLog : MonoBehaviour
{
    private bool isLogOpen;




    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }


    public void OpenAndCloseLog()
    {
        isLogOpen = !isLogOpen;

        if (isLogOpen)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}








   