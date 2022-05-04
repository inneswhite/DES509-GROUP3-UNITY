using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInvestigateButton : MonoBehaviour
{
    [SerializeField] Sprite idle;
    [SerializeField] Sprite hover;
    [SerializeField] Sprite pressed;
    [SerializeField] Sprite selected;

    Image image; 


    enum ActiveSprite
    {
        idle,
        hover,
        pressed,
        selected
    }
    ActiveSprite activeSprite;

    bool isSelected = false;

    private void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }
    private void Start()
    {
        SetIdle();
    }

    public void SetIdle()
    {
        if (activeSprite != ActiveSprite.idle && !isSelected)
        {
            image.sprite = idle;
            activeSprite = ActiveSprite.idle;
        }
        if (isSelected)
        {
            image.sprite = selected;
            activeSprite = ActiveSprite.selected;
        }

    }

    public void SetHover()
    {
        if (activeSprite != ActiveSprite.hover)
        {
            image.sprite = hover;
            activeSprite = ActiveSprite.hover;
        }
    }

    public void SetPressed()
    {
        if (activeSprite != ActiveSprite.pressed )
        {
            image.sprite = pressed;
            activeSprite = ActiveSprite.pressed;

            if (isSelected)
            {
                isSelected = false;
            }
            else
            {
                isSelected = true;
            }
        }
    }

    public void SetSelected()
    {
        if (activeSprite != ActiveSprite.selected)
        {
            image.sprite = selected;
            activeSprite = ActiveSprite.selected;
        }
    }
}
