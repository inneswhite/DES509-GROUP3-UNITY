using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRuleBookManager : MonoBehaviour
{
    private static UIRuleBookManager _instance;
    public static UIRuleBookManager instance { get { return _instance; } }

    RectTransform rectTransform;
    Vector2 activePos, inactivePos;
    UIManager uiManager;

    [SerializeField]
    private bool rulebookActive = true;
    [SerializeField] UIRuleButton[] ruleButtons = new UIRuleButton[8];
    public UIRuleButton selectedRule;
    public int rulenumber;
    [SerializeField]
    private InspectManager inspectmanager;
    [SerializeField]
    private GameObject Associated;
    private bool isAssociated;
    private void Awake()
    {
        SingletonCheck();
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    private void Start()
    {
        uiManager = UIManager.instance;
        activePos = rectTransform.position;

        inactivePos = activePos - (Vector2.right * uiManager.GetCanvasSize());
        Associated.SetActive(false);
        //Deactivate();
    }
    public void Activate()
    {
        rectTransform.position = activePos;
        rulebookActive = true;
    }

    public void Deactivate()
    {
        rectTransform.position = inactivePos;
        selectedRule = null;
        rulebookActive = false;
    }

    //this method is called by the UI Rule buttons.
    public void RuleSelected(UIRuleButton _ruleButton)
    {
        //if no rule has been selected yet, then assign the newly selected rule to "selectedRule" variable
        //if there has already been a rule selected, unselect it before assigning the new rule to "selectedRule"
        if (selectedRule == null)
        {
            selectedRule = _ruleButton;
            rulenumber = _ruleButton.value;
        }
        else if (selectedRule != _ruleButton)
        {
            selectedRule.Unselected();
            selectedRule = _ruleButton;
            rulenumber = _ruleButton.value;
        }

        if (selectedRule = _ruleButton)
        {
            if (inspectmanager.itemid == 0)
            {
                if (rulenumber == 6)
                {
                    isAssociated = true;
                    if (isAssociated)
                    {
                        Associated.SetActive(true);
                    }
                }
                else if (rulenumber != 6)
                {
                    isAssociated = false;
                    if (!isAssociated)
                    {
                        Associated.SetActive(false);
                    }
                }
            }
            if (inspectmanager.itemid == 1)
            {
                if (rulenumber == 3)
                {
                    isAssociated = true;            // based on item id and rule number turn bool on
                    if (isAssociated)
                    {
                        Associated.SetActive(true);
                    }
                }
                else if (rulenumber != 3)
                {
                    isAssociated = false;
                    if (!isAssociated)
                    {
                        Associated.SetActive(false);
                    }
                }
            }
        }
    }

    //Retruns the activity state of the Rulebook menu
    public bool rulebookIsOpen()
    {
        return rulebookActive;
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

