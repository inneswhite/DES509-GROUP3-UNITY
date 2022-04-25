using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIFadePanel : MonoBehaviour
{
    private static UIFadePanel _instance;
    public static UIFadePanel instance { get { return _instance; } }
    [SerializeField] Image fadePanel;

    [SerializeField] float fadeDuration = 1f;
    [SerializeField] LeanTweenType leanTweenType;

    private void Awake()
    {
        SingletonCheck();
    }

    private void Start()
    {
        SceneFadeIn();
    }

    void SingletonCheck()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void SceneFadeIn()
    {
        LeanTween.value(1f, 0f, fadeDuration).setOnUpdate
                (
                    (float _alpha) =>
                    {
                        fadePanel.color = new Color(0f, 0f, 0f, _alpha);
                    }
                ).setEase(leanTweenType);

    }

    public void SceneFadeIn(float _fadeDuration)
    {
        LeanTween.value(1f, 0f, _fadeDuration).setOnUpdate
                (
                    (float _alpha) =>
                    {
                        fadePanel.color = new Color(0f, 0f, 0f, _alpha);
                    }
                ).setEase(leanTweenType);

    }

    public void SceneFadeOut()
    {
        LeanTween.value(0f, 1f, fadeDuration).setOnUpdate
                (
                    (float _alpha) =>
                    {
                        fadePanel.color = new Color(0f, 0f, 0f, _alpha);
                    }
                ).setEase(leanTweenType);
    }

    public void SceneFadeOut(float _fadeDuration)
    {
        LeanTween.value(0f, 1f, _fadeDuration).setOnUpdate
                (
                    (float _alpha) =>
                    {
                        fadePanel.color = new Color(0f, 0f, 0f, _alpha);
                    }
                ).setEase(leanTweenType);
    }
}
