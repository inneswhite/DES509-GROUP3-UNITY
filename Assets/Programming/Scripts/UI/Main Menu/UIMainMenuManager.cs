using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIMainMenuManager : MonoBehaviour
{
    [SerializeField] UIMainMenuPanel uIMainMenuPanel;
    [SerializeField] UISettingsPanel uiSettingsPanel;
    [SerializeField] UIPhonePanel uIPhonePanel;

    enum ActivePanel
    {
        none,
        mainMenu,
        settings,
        phone
    }
    ActivePanel activePanel = ActivePanel.none;

    private void Start()
    {
        OpenMainMenu();
    }
    public void OpenPhone()
    {
        if (activePanel == ActivePanel.mainMenu)
        {
            StartCoroutine(OpenPhoneSequence());
            activePanel = ActivePanel.phone;
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartGameSequence());
    }

    IEnumerator StartGameSequence()
    {
        UIFadePanel.instance.SceneFadeOut(1f);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);
    }

    IEnumerator OpenPhoneSequence()
    {
        uIMainMenuPanel.Deactivate();
        OpenPhonePanel();
        //UIFadePanel.instance.SceneFadeOut(1f);
        yield return new WaitForSeconds(1f);
        //
    }

    public void OpenPhonePanel()
    {
        if(activePanel != ActivePanel.phone)
        {
            uIPhonePanel.Activate();
            activePanel = ActivePanel.phone;
        }
    }
    public void OpenMainMenu()
    {
        if (activePanel != ActivePanel.mainMenu)
        {
            uIMainMenuPanel.Activate();
            activePanel = ActivePanel.mainMenu;
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


}
