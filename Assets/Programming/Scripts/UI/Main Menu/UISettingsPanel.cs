using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISettingsPanel : MonoBehaviour
{
    GameObject[] settingsPanelChildren;


    private void Awake()
    {
        
    }

    private void Start()
    {
        settingsPanelChildren = new GameObject[transform.childCount];
        for (int i = 0; i < settingsPanelChildren.Length; i++)
        {
            settingsPanelChildren[i] = transform.GetChild(i).gameObject;
            settingsPanelChildren[i].SetActive(false);
        }
    }
    public void Activate()
    {
        for(int i = 0; i < settingsPanelChildren.Length; i++)
        {
            settingsPanelChildren[i].SetActive(true);
        }
    }
}
