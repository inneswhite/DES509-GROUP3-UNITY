using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UISettingsPanel : MonoBehaviour
{
    List<PauseUIButton> menuButtons = new List<PauseUIButton>();
    [SerializeField] float buttonFadeDuration = 0.5f;
    [SerializeField] LeanTweenType leanTweenType;
    [SerializeField] float menuOpenDelay = 1f;

    [Header("Slider Reference")]
    [SerializeField] Slider slider;
    [SerializeField] Image[] sliderElements = new Image[3];

    bool isActive = false;
    private void Awake()
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
            menuButton.SetAlpha(0);
            menuButton.gameObject.SetActive(false);
        }

        for(int i = 0; i < sliderElements.Length; i++)
        {
            sliderElements[i].color = new Color(sliderElements[i].color.r, sliderElements[i].color.g, sliderElements[i].color.b, 0);
        }
        slider.gameObject.SetActive(false);
    }

    public void Activate()
    {
        //Cycle through each child with a menu button component, and fade them in. 
        if (!isActive)
        {
            StartCoroutine(ActivationSequence());
            slider.gameObject.SetActive(true);
            isActive = true;
        }
    }

    IEnumerator ActivationSequence()
    {
        yield return new WaitForSecondsRealtime(menuOpenDelay);
        foreach (PauseUIButton menuButton in menuButtons)
        {
            menuButton.gameObject.SetActive(true);
            menuButton.Enable();
            menuButton.SetAlpha(0f);
            LeanTween.value(0f, 1f, buttonFadeDuration).setOnUpdate
               (
                   (float _alpha) =>
                   {
                       menuButton.SetAlpha(_alpha);
                   }
               ).setEase(leanTweenType).setIgnoreTimeScale(true);
        }
        LeanTween.value(0f, 1f, buttonFadeDuration).setOnUpdate
               (
                   (float _alpha) =>
                   {
                       for (int i = 0; i < sliderElements.Length; i++)
                       {
                           sliderElements[i].color = new Color(sliderElements[i].color.r, sliderElements[i].color.g, sliderElements[i].color.b, _alpha);
                       }
                   }
               ).setEase(leanTweenType).setIgnoreTimeScale(true);

    }

    public void Deactivate()
    {
        StartCoroutine(DeactivationSequence());
    }

    IEnumerator DeactivationSequence()
    {
        foreach (PauseUIButton menuButton in menuButtons)
        {
            menuButton.SetAlpha(1f);
            menuButton.Disable();
            
            LeanTween.value(1f, 0f, buttonFadeDuration).setOnUpdate
               (
                   (float _alpha) =>
                   {
                       menuButton.SetAlpha(_alpha);
                   }
               ).setEase(leanTweenType).setIgnoreTimeScale(true);

            isActive = false;
        }
        LeanTween.value(1f, 0f, buttonFadeDuration).setOnUpdate
               (
                   (float _alpha) =>
                   {
                       for (int i = 0; i < sliderElements.Length; i++)
                       {
                           sliderElements[i].color = new Color(sliderElements[i].color.r, sliderElements[i].color.g, sliderElements[i].color.b, _alpha);
                       }
                   }
               ).setEase(leanTweenType).setIgnoreTimeScale(true);

        yield return new WaitForSecondsRealtime(buttonFadeDuration);

        foreach (PauseUIButton menuButton in menuButtons)
        {
            menuButton.gameObject.SetActive(false);
        }
        slider.gameObject.SetActive(false);
    }
}
