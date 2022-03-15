using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationParams : MonoBehaviour
{
    Animator animator;
    CharacterController characterController;
    float speed = 0;
    [Header("Debug")]
    [SerializeField] private bool showPlayerSpeed = false;

    [Header("Animation Settings")]
    [SerializeField]
    float idlePlaybackSpeed = 1f;
    [SerializeField]
    float walkPlaybackSpeed = 1f,
        jogPlaybackSpeed = 1f;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        characterController = gameObject.GetComponent<CharacterController>();

    }
    private void Start()
    {
        StartAnimationValues();
    }

    private void FixedUpdate()
    {
        UpdateAnimationValues();
        ShowDebug();
    }


    //____________________________Custom Functions____________________________\\

    private float GetPlayerSpeed()
    {
        float _speed = characterController.velocity.magnitude;
        return _speed;
    }

    private void StartAnimationValues()
    {
        animator.SetFloat("idleAnimSpeed", idlePlaybackSpeed);
        animator.SetFloat("walkAnimSpeed", walkPlaybackSpeed);
        animator.SetFloat("jogAnimSpeed", jogPlaybackSpeed);
    }

    private void UpdateAnimationValues()
    {
        animator.SetFloat("moveSpeed", GetPlayerSpeed());
        animator.SetFloat("idleAnimSpeed", idlePlaybackSpeed * GetPlayerSpeed());
        animator.SetFloat("walkAnimSpeed", walkPlaybackSpeed * GetPlayerSpeed());
        animator.SetFloat("jogAnimSpeed", jogPlaybackSpeed * GetPlayerSpeed());
    }

    void ShowDebug()
    {
        if (showPlayerSpeed)
        {
            Debug.Log("Player Speed: " + GetPlayerSpeed());
        }
    }
}
