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

    private void Awake()
    {
        text = gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

     //____________Event Triggers___________\\
    //See Event Trigger Component for details\\
    public void PointerEnter()
    {
        text.color = hoverColor;
    }

    public void PointerExit()
    {
        text.color = defaultColor;
    }

    public void PointerDown()
    {
        text.color = pressedColor;
    }

}
