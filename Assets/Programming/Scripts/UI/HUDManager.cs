using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField]
    HUDMenuPanel objectivesPanel;
    [SerializeField]
    HUDMenuPanel inventoryPanel;

    private enum ActivePanel
    {
        objectives,
        inventory,
        none
    };

    [SerializeField]
    ActivePanel activePanel = ActivePanel.none;

    public void ActivateObjectivesPanel()
    {
        if (activePanel == ActivePanel.none)
        {
            objectivesPanel.Activate();
            activePanel = ActivePanel.objectives;
        }
    }
    public void DeactivateObjectivesPanel()
    {
        if(activePanel == ActivePanel.objectives)
        {
            objectivesPanel.Deactivate();
            activePanel = ActivePanel.none;
        }
    }

    public void ActivateInventoryPanel()
    {
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
            inventoryPanel.Deactivate();
            activePanel = ActivePanel.none;
        }
    }
}
