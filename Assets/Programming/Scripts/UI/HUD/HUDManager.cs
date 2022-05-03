using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//HUDManager communicates between UI Manager and HUD elements
public class HUDManager : MonoBehaviour
{

    public AK.Wwise.Event SoundInt;
    public AK.Wwise.Event SoundInv;

    [Header("UI References")]
    [SerializeField]
    HUDMenuPanel objectivesPanel;
    [SerializeField] 
    HUDMenuButton objectivesButton;
    [SerializeField]
    HUDMenuPanel inventoryPanel;
    [SerializeField]
    HUDMenuButton inventoryButton;



    public enum ActivePanel
    {
        objectives,
        inventory,
        none
    };

    [SerializeField]
    ActivePanel activePanel = ActivePanel.none;

    //Activate Objects Panel | Only runs if no panel is open
    public void ActivateObjectivesPanel()
    {
        if(activePanel == ActivePanel.inventory)
        {
            DeactivateInventoryPanel();
        }
        if (activePanel == ActivePanel.none)
        {
            objectivesPanel.Activate();
            activePanel = ActivePanel.objectives;
        }
    }

    //Deactivate Objects Panel | Only runs if objects panel is open
    public void DeactivateObjectivesPanel()
    {
        if(activePanel == ActivePanel.objectives)
        {
            objectivesButton.Deselect();
            objectivesPanel.Deactivate();
            activePanel = ActivePanel.none;
        }
    }

    public void ActivateInventoryPanel()
    {
        if(activePanel == ActivePanel.objectives)
        {
            DeactivateObjectivesPanel();
        }

        if (activePanel == ActivePanel.none)
        {
            inventoryPanel.Activate();
            activePanel = ActivePanel.inventory;
        }
        
    }
    public void DeactivateInventoryPanel()
    {
        if(activePanel == ActivePanel.inventory)
        {
            inventoryButton.Deselect();
            inventoryPanel.Deactivate();
            activePanel = ActivePanel.none;
        }
    }

    public bool menuIsOpen()
    {
        if (activePanel == ActivePanel.none)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // Close any menu that may be open
    public void CloseMenus()
    {
        if(activePanel == ActivePanel.objectives)
        {
            DeactivateObjectivesPanel();
        }
        else if (activePanel == ActivePanel.inventory)
        {
            DeactivateInventoryPanel();
        }
    }

    public void InteractSound()
    {
        SoundInt.Post(gameObject);
    }

    public void InvestigateSound()
    {
        SoundInv.Post(gameObject);
    }
}
