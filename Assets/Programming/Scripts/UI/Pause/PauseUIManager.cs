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

    [SerializeField] UISettingsPanel uiSettingsPanel;

    [Header("Fade Settings")]
    [SerializeField] float buttonFadeDuration = 1f;
    [SerializeField] LeanTweenType leanTweenType = LeanTweenType.easeInOutQuad;

    List<PauseUIButton> menuButtons = new List<PauseUIButton>();

    enum ActivePanel
    {
        none,
        pauseMenu,
        settings,
    }
    ActivePanel activePanel = ActivePanel.none;

    private void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    private void Start()
    {
        uiManager = UIManager.instance;
        activePos = rectTransform.position;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<PauseUIButton>())
            {
                menuButtons.Add(transform.GetChild(i).GetComponent<PauseUIButton>());

            }
        }

        foreach (PauseUIButton menuButton in menuButtons)
        {
            menuButton.Disable();
        }

        inactivePos = activePos - (Vector2.right * uiManager.GetCanvasSize());
        activePanel = ActivePanel.none;
        Deactivate();
    }
    public void Activate()
    {
        Time.timeScale = 0f;        //pause
        rectTransform.position = activePos;
        pauseActive = true;

        foreach (PauseUIButton menuButton in menuButtons)
        {
            menuButton.Enable();
        }
        activePanel = ActivePanel.pauseMenu;

    }

    public void Deactivate()
    {
        foreach (PauseUIButton menuButton in menuButtons)
        {
            menuButton.Disable();
        }

        Time.timeScale = 1f;       //resume
        rectTransform.position = inactivePos;
        pauseActive = false;
        activePanel = ActivePanel.none;
    }

    public void HideMainPause()
    {
        foreach (PauseUIButton menuButton in menuButtons)
        {
            menuButton.Disable();
            menuButton.SetAlpha(1f);
            LeanTween.value(1f, 0f, buttonFadeDuration).setOnUpdate
               (
                   (float _alpha) =>
                   {
                       menuButton.SetAlpha(_alpha);
                   }
               ).setEase(leanTweenType).setIgnoreTimeScale(true);
        }
    }

    public void ShowMainPause()
    {
        foreach (PauseUIButton menuButton in menuButtons)
        {
            menuButton.Enable();
            menuButton.SetAlpha(0f);
            LeanTween.value(0f, 1f, buttonFadeDuration).setOnUpdate
               (
                   (float _alpha) =>
                   {
                       menuButton.SetAlpha(_alpha);
                   }
               ).setEase(leanTweenType).setIgnoreTimeScale(true);
        }
    }


    public void OpenSettings()
    {
        if (activePanel != ActivePanel.settings)
        {
            uiSettingsPanel.Activate();
            activePanel = ActivePanel.settings;
        }
        
    }
    public void CloseSettings()
    {
        if (activePanel == ActivePanel.settings)
        {
            uiSettingsPanel.Deactivate();

        }
    }


    //Retruns the activity state of the pause menu
    public bool pauseMenuIsOpen()
    {
        return pauseActive;
    }

    public void ExitToDesktop()
    {
        Application.Quit();
    }
}
