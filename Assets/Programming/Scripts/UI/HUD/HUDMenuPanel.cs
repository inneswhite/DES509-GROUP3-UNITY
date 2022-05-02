using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDMenuPanel : MonoBehaviour
{
    UIManager uiManager;
    [Header("Animation Parameters")]
    [SerializeField]
    float animDuration = 1f;
    [SerializeField]
    LeanTweenType leanTweenType;

    Vector2 activePosition, inactivePosition;
    RectTransform rectTransform;



    bool isActive = false;
    private void Awake()
    {
        
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    private void Start()
    {
        uiManager = UIManager.instance;
        activePosition = rectTransform.position;
        inactivePosition = new Vector2(-rectTransform.sizeDelta.x - 80, activePosition.y);
        rectTransform.position = inactivePosition;
    }


    public void Activate()
    {
        if (!isActive)
        {
            LeanTween.value(inactivePosition.x, activePosition.x, animDuration).setOnUpdate
                (
                    (float _xPos) =>
                    {
                        rectTransform.position = new Vector2(_xPos, activePosition.y);
                    }
                ).setEase(leanTweenType);
            isActive = true;
        }
    }

    public void Deactivate() 
    {
        if (isActive)
        {
            LeanTween.value(activePosition.x, inactivePosition.x, animDuration).setOnUpdate
                (
                    (float _xPos) =>
                    {
                        rectTransform.position = new Vector2(_xPos, inactivePosition.y);
                    }
                ).setEase(leanTweenType);
            isActive = false;
        }
    }

    private void OnDisable()
    {
        Debug.LogError("");
    }
}
