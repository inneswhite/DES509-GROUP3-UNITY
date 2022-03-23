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

    Button button;
    TMPro.TextMeshProUGUI text;

    private void Awake()
    {
        button = gameObject.GetComponent<Button>();
        text = gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

     //____________Event Triggers___________\\
    //See Event Trigger Component for details\\
    public void PointerEnter()
    {
        text.color = hoverColor;
        Debug.Log("pointer enter");
    }

    public void PointerExit()
    {
        text.color = defaultColor;
        Debug.Log("pointer exit");
    }

    public void PointerDown()
    {
        text.color = pressedColor;
    }

}
