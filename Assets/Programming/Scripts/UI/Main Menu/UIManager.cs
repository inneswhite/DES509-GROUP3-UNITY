using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    private static UIManager _instance;
    public static UIManager instance { get { return _instance; } }
    [SerializeField]
    HUDManager hud;
    [SerializeField]
    PauseUIManager pauseUIManager;

    RectTransform rectTransform;

    public GameObject pausePanel;

    private void Awake()
    {
        SingletonCheck();
        
        rectTransform = gameObject.GetComponent<RectTransform>();
    }



    //Temporary Input
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
       
            if (hud.menuIsOpen())
            {
                CloseHUDMenus();
            }
            else if (!pauseUIManager.pauseMenuIsOpen())
            {

                ActivatePauseMenu();
            }else if (pauseUIManager.pauseMenuIsOpen())
            {
                ClosePauseMenu();
                
            }
        }
    }


    // Activates Pause Menu | Only runs if HUD Menus are closed
    public void ActivatePauseMenu()
    {
        if (!hud.menuIsOpen())
        {
           
            pauseUIManager.Activate();
        }
    }

    public void ClosePauseMenu()
    {
        if (pauseUIManager.pauseMenuIsOpen())
        {
            pauseUIManager.Deactivate();
        }
    }
    public void CloseHUDMenus()
    {
        if (hud.menuIsOpen())
        {
            hud.CloseMenus();
        }
    }

    public Vector2 GetCanvasSize()
    {
        return rectTransform.sizeDelta;
    }

    public void ExitToDesktop()
    {
        Application.Quit();
    }

    void SingletonCheck()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
