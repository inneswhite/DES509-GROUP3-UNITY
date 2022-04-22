using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UIRuleButton : MonoBehaviour
{
    [Header("Button Text Colours")]
    [SerializeField]
    Color defaultColor = Color.white;
    [SerializeField]
    Color hoverColor = Color.grey;
    [SerializeField]
    Color pressedColor = Color.white;

    TMPro.TextMeshProUGUI text;
    public bool ruleSelected = false;

    UIRuleBookManager uiRuleBookManager;

    [SerializeField]
    internal int value;

    private void Awake()
    {
        text = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void Start()
    {
        uiRuleBookManager = UIRuleBookManager.instance;
    }

    //____________Event Triggers___________\\
    //See Event Trigger Component for details\\
    public void PointerEnter()
    {
        if (!ruleSelected)
        {
            text.color = hoverColor;
        }
    }

    public void PointerExit()
    {
        if (!ruleSelected)
        {
            text.color = defaultColor;
        }
    }

    public void PointerDown()
    {
        text.color = pressedColor;
        if (!ruleSelected)
        {
            uiRuleBookManager.RuleSelected(this);
            ruleSelected = true;
        }
        
    }

    public void Unselected()
    {
        ruleSelected = false;
        text.color = defaultColor;
    }
}
