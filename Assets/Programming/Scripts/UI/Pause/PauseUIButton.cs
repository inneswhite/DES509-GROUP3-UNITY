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

    private void Start()
    {
        
    }

}
