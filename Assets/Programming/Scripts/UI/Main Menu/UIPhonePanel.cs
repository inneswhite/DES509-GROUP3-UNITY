using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPhonePanel : MonoBehaviour
{
    [SerializeField] PauseUIButton continueButton, beginDayButton;
    [SerializeField] float buttonFadeDuration = 1f;
    [SerializeField] LeanTweenType leanTweenType;
    [SerializeField] UIPhone uIPhone;

    private void Start()
    {
        continueButton.SetAlpha(0);
        beginDayButton.SetAlpha(0);
        continueButton.Disable();
        beginDayButton.Disable();
    }
    public void Activate()
    {
        StartCoroutine(ActivateEvents());
        
    }

    IEnumerator ActivateEvents()
    {
        yield return new WaitForSeconds(1f);
        uIPhone.Activate();
        yield return new WaitForSeconds(2f);
        continueButton.Enable();
        LeanTween.value(0f, 1f, buttonFadeDuration).setOnUpdate
               (
                   (float _alpha) =>
                   {
                       continueButton.SetAlpha(_alpha);
                   }
               ).setEase(leanTweenType);
        yield return 0;
    }

    public void OpenMessage()
    {
        StartCoroutine(OpenMessageEvents());
    }

    IEnumerator OpenMessageEvents()
    {
        LeanTween.value(1f, 0f, buttonFadeDuration).setOnUpdate
               (
                   (float _alpha) =>
                   {
                       continueButton.SetAlpha(_alpha);
                   }
               ).setEase(leanTweenType);

        yield return new WaitForSeconds(buttonFadeDuration);

        continueButton.Disable();
        continueButton.gameObject.SetActive(false);
        uIPhone.ShowMessage();

        yield return new WaitForSeconds(2f);

        beginDayButton.Enable();
        LeanTween.value(0f, 1f, buttonFadeDuration).setOnUpdate
               (
                   (float _alpha) =>
                   {
                       beginDayButton.SetAlpha(_alpha);
                   }
               ).setEase(leanTweenType);
    }
}
