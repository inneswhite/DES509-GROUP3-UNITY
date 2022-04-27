using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PauseUIButton : MonoBehaviour
{
    [Header("Button Text Colours")]
    [SerializeField]
    Color defaultColor = Color.white;
    [SerializeField]
    Color hoverColor = Color.grey;
    [SerializeField]
    Color pressedColor = Color.white;

    TMPro.TextMeshProUGUI text;
    Image image;

    [SerializeField] bool isEnabled = false;

    private void Awake()
    {
        text = gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        image = gameObject.GetComponent<Image>();
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
        image.raycastTarget = true;
        
    }

    public void Disable()
    {
        isEnabled = false;
        image.raycastTarget = false;
    }

    public void SetAlpha(float _alpha)
    {
        Color _tempColor = new Color(text.color.r, text.color.g, text.color.b, _alpha);
        text.color = _tempColor;
    }

}
