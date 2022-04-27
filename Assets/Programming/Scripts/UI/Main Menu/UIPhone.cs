using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPhone : MonoBehaviour
{
    Animator animator;
    Material material;
    

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        material = gameObject.GetComponent<MeshRenderer>().materials[1];
    }
    public void Activate()
    {
        animator.SetBool("phoneActive", true);
    }
    public void Deactivate()
    {
        animator.SetBool("phoneActive", false);
    }

    public void ShowMessage()
    {
        LeanTween.value(0f, 1f, 1f).setOnUpdate
                  (
                      (float _blend) =>
                      {
                          material.SetFloat("_Blend", _blend);
                      }
                  ).setEase(LeanTweenType.easeInOutQuad);
        
    }
}
