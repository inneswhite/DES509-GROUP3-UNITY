using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUIManager : MonoBehaviour
{
    RectTransform rectTransform;
    Vector2 activePos, inactivePos;
    UIManager uiManager;

    [SerializeField]
    private bool pauseActive = false;

    private void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    private void Start()
    {
        uiManager = UIManager.instance;
        activePos = rectTransform.position;
        
        inactivePos = activePos - (Vector2.right * uiManager.GetCanvasSize());

        Deactivate();
    }
    public void Activate()
    {
        Time.timeScale = 0f;        //pause
        rectTransform.position = activePos;
        pauseActive = true;
    }

    public void Deactivate()
    {
        Time.timeScale = 1f;       //resume
        rectTransform.position = inactivePos;
        pauseActive = false;
    }


    //Retruns the activity state of the pause menu
    public bool pauseMenuIsOpen()
    {
        return pauseActive;
    }
}
