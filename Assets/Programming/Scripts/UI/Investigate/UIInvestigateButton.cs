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

    [Header("Animation Parameters")]
    [SerializeField] float buttonFadeDuration = 0.75f;


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
    bool isActive = false;

    private void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }
    private void Start()
    {
        image.raycastTarget = false;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
    }


    public void Activate()
    {
        if (!isActive) { 
            SetIdle();
            image.raycastTarget = true;
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
            LeanTween.value(0f, 1f, buttonFadeDuration).setOnUpdate
                (
                    (float _alpha) =>
                    {
                        image.color = new Color(image.color.r, image.color.g, image.color.b, _alpha);
                    }
                ).setEase(LeanTweenType.easeInOutQuad);
            isActive = true;
        }
    }

    public void Deactivate()
    {
        if (isActive)
        {
            SetIdle();
            image.raycastTarget = false;
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
            LeanTween.value(1f, 0f, buttonFadeDuration).setOnUpdate
                (
                    (float _alpha) =>
                    {
                        image.color = new Color(image.color.r, image.color.g, image.color.b, _alpha);
                    }
                ).setEase(LeanTweenType.easeInOutQuad);
            isActive = false;
        }
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
