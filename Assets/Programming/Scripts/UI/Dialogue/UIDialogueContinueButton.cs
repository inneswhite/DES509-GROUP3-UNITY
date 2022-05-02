using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogueContinueButton : MonoBehaviour
{
    [Header("Button Text Colours")]
    [SerializeField]
    Color defaultColor = Color.white;
    [SerializeField]
    Color hoverColor = Color.grey;
    [SerializeField]
    Color pressedColor = Color.white;

    TMPro.TextMeshProUGUI text;

    [SerializeField] bool isEnabled = true;

    private void Awake()
    {
        text = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

    //____________Event Triggers___________\\
    //See Event Trigger Component for details\\
    public void PointerEnter()
    {
        Debug.Log("Pointer Enter: " + gameObject.name);
        if (isEnabled)
        {
            text.color = hoverColor;
        }
    }

    public void PointerExit()
    {
        if (isEnabled)
        {
            text.color = defaultColor;
        }
    }

    public void PointerDown()
    {
        if (isEnabled)
        {
            text.color = pressedColor;
        }
    }

    public void Enable()
    {
        isEnabled = true;
        text.color = defaultColor;
    }

    public void Disable()
    {
        isEnabled = false;
    }

}
