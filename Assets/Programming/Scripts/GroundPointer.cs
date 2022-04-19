using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPointer : MonoBehaviour
{
    private Transform player;
    private float dist;
    
    [SerializeField][Tooltip("Set the distance from player that this pointer will be destroyed at")] float destroyDistance = 1f;
    [Header("Animation Parameters")]
    [SerializeField][Tooltip("Scale of pointer graphic")] float growRadius = 4f;
    [SerializeField] float growAnimDuration = 1f, shrinkAnimDuration = 2f;
   
    [SerializeField] LeanTweenType leanTweenType = LeanTweenType.easeInExpo;

    bool destroyAnimationRunning = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.localScale = Vector3.zero;
        StartCoroutine(ScaleInAnimation());
    }

    void Update()
    {
        dist = Vector3.Distance(transform.position, player.position);
        if (dist<= destroyDistance && !destroyAnimationRunning)
        {
            StartCoroutine(ScaleOutAnimation());
            destroyAnimationRunning = true;
        }
    }

    IEnumerator ScaleOutAnimation()
    {
        LeanTween.scale(gameObject, Vector3.one * growRadius, growAnimDuration).setEase(leanTweenType);
        yield return new WaitForSecondsRealtime(growAnimDuration);
        LeanTween.scale(gameObject, Vector3.zero, shrinkAnimDuration).setEase(leanTweenType);
        yield return new WaitForSecondsRealtime(shrinkAnimDuration);
        Destroy(gameObject);
    }

    IEnumerator ScaleInAnimation()
    {
        LeanTween.scale(gameObject, Vector3.one * growRadius, shrinkAnimDuration*0.5f).setEase(leanTweenType);
        yield return new WaitForSecondsRealtime(growAnimDuration*0.5f);
        LeanTween.scale(gameObject, Vector3.one, growAnimDuration*0.5f).setEase(leanTweenType);
    }
}
