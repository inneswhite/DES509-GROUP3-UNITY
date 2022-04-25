using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFadePanelOnAwake : MonoBehaviour
{
    private void Start()
    {
        if (UIFadePanel.instance)
        {
            UIFadePanel.instance.SceneFadeIn();
        }
    }
}
