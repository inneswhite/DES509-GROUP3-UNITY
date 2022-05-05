using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenuPanel : MonoBehaviour
{
    List<PauseUIButton> menuButtons = new List<PauseUIButton>();
    [SerializeField] Image titleImage, backgroundImage;
    [SerializeField] float buttonFadeDuration = 0.5f;
    [SerializeField] LeanTweenType leanTweenType;

    bool isActive = false;
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<PauseUIButton>())
            {
                menuButtons.Add(transform.GetChild(i).GetComponent<PauseUIButton>());
            }
        }

        foreach (PauseUIButton menuButton in menuButtons)
        {
            menuButton.Disable();
        }
    }

    public void Activate()
    {
        if (!isActive)
        {
            foreach (PauseUIButton menuButton in menuButtons)
            {
                menuButton.Enable();
                LeanTween.value(0f, 1f, buttonFadeDuration).setOnUpdate
                   (
                       (float _alpha) =>
                       {
                           menuButton.SetAlpha(_alpha);
                       }
                   ).setEase(leanTweenType);
            }
            LeanTween.value(0f, 1f, buttonFadeDuration).setOnUpdate
                   (
                       (float _alpha) =>
                       {
                           titleImage.color = new Color(titleImage.color.r, titleImage.color.g, titleImage.color.b, _alpha);
                           backgroundImage.color = new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, _alpha);
                       }
                   ).setEase(leanTweenType);
            
            isActive = true;
        }
    }

    public void Deactivate()
    {
        foreach (PauseUIButton menuButton in menuButtons)
        {
            menuButton.Disable();
            LeanTween.value(1f, 0f, buttonFadeDuration).setOnUpdate
               (
                   (float _alpha) =>
                   {
                       menuButton.SetAlpha(_alpha);
                   }
               ).setEase(leanTweenType);
            LeanTween.value(1f, 0f, buttonFadeDuration).setOnUpdate
                   (
                       (float _alpha) =>
                       {
                           titleImage.color = new Color(titleImage.color.r, titleImage.color.g, titleImage.color.b, _alpha);
                           backgroundImage.color = new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, _alpha);
                       }
                   ).setEase(leanTweenType);

            isActive = false;
        }
    }
}
