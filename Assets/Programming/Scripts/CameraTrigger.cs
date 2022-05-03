using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private Camera camid, camid2;
    [Header("Camera Follow")]
    [SerializeField]
    private CameraFollow sidecam;
    [SerializeField]
    private CameraFollow maincam;
    [SerializeField]
    private int camfollowvalue;
    void Start()
    {
        sidecam.enabled = false;
       /* if (canvas == null) 
            canvas.gameObject.GetComponent<Canvas>();
        CanvasGroup canvasGroup = canvas.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            Debug.LogError("Please assign a canvas group to the canvas!");

        if (animationCurve.length == 0)
        {
            Debug.Log("Animation curve not assigned: Create a default animation curve");
            animationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        }

        StartCoroutine(FadeCanvas(canvasGroup, Direction.FadeIn, fadingSpeed));
        //StartCoroutine(FadeCanvas(canvasGroup, Direction.FadeOut, fadingSpeed)); */
    }

 /*   public IEnumerator FadeCanvas(CanvasGroup canvasGroup, Direction direction, float duration)
    {
        yield return new WaitForSeconds(5f);
        // fading start and finish
        float beginTime = Time.time;
        float  endTime = Time.time + duration;
        float  elapsedTime = 0f;

        // set the canvas to the start alpha
        if (direction == Direction.FadeIn) canvasGroup.alpha = animationCurve.Evaluate(0f);
        else canvasGroup.alpha = animationCurve.Evaluate(1f);

        // loop repeatedly until the previously calculated end time
        while (Time.time <= endTime)
        {
            elapsedTime = Time.time - beginTime; // update the elapsed time
            var percentage = 1 / (duration / elapsedTime); // calculate how far along the timeline we are
            if ((direction == Direction.FadeOut)) // if we are fading out
            {
                canvasGroup.alpha = animationCurve.Evaluate(1f - percentage);
            }
            else // if we are fading 
            {
                canvasGroup.alpha = animationCurve.Evaluate(percentage);
            }

            yield return new WaitForEndOfFrame(); // wait for the next frame before continuing the loop
        }

        // end alpha
        if (direction == Direction.FadeIn) canvasGroup.alpha = animationCurve.Evaluate(1f);
        else canvasGroup.alpha = animationCurve.Evaluate(0f);
    } */



private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.tag=="Player")
        {
            if(camfollowvalue==1)
            {
                maincam.enabled = true;
                sidecam.enabled = false;
                camid.enabled = true;                   // Switch Cameras
                camid2.enabled = false;
            }
            if (camfollowvalue == 2)
            {
                maincam.enabled = true;
                sidecam.enabled = false;
                camid.enabled = true;                   // Switch Cameras
                camid2.enabled = false;
            }
            if (camfollowvalue == 3)
            {
                sidecam.enabled = true;
                maincam.enabled = false;
                camid.enabled = true;                   // Switch Cameras
                camid2.enabled = false;
            }
            if (camfollowvalue == 4)
            {
                sidecam.enabled = false;
                maincam.enabled = false;
                camid.enabled = true;                   // Switch Cameras
                camid2.enabled = false;
            }

        }
    }
}
