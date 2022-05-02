using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDMenuButton : MonoBehaviour
{
    [Header("Button Graphics")]
    [SerializeField]
    Sprite defaultImage;
    [SerializeField]
    Sprite hoverImage, pressedImage, selectedImage;

    [SerializeField] bool isSelected = false;

    Image activeImage;

    private void Awake()
    {
        activeImage = gameObject.GetComponent<Image>();
    }

    private void Start()
    {
        activeImage.sprite = defaultImage;
    }

    //____________Event Triggers___________\\
    //See Event Trigger Component for details\\

    public void PointerEnter()
    {
        if (!isSelected)
        {
            activeImage.sprite = hoverImage;
        }
    }

    public void PointerExit()
    {
        if (!isSelected)
        {
            activeImage.sprite = defaultImage;
        }
    }

    public void PointerDown()
    {
        if (!isSelected)
        {
            activeImage.sprite = pressedImage;
        }
    }

    public void PointerClick()
    {
        if (!isSelected)
        {
            Selected();
        }
    }

    void Selected()
    {
        activeImage.sprite = selectedImage;
        isSelected = true;
    }

    public void Deselect()
    {
        isSelected = false;
        activeImage.sprite = defaultImage;
    }
    public void SetAlpha(float _alpha)
    {
        Color _tempColor = new Color(activeImage.color.r, activeImage.color.g, activeImage.color.b, _alpha);
        activeImage.color = _tempColor;
    }
}
