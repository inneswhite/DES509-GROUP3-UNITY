using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingLantern : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();

    }
    private void Start()
    {
        animator.SetFloat("animOffset", Random.value);
    }
}
