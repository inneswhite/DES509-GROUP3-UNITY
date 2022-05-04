using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FrameCount : MonoBehaviour
{
    Text text;
    string startText;

    int numberOfFrames = 0;
    float timeSinceLastFrame = 0;

    enum FrameCountType
    {
        fps,
        average
    }

    int fps;

    [SerializeField] FrameCountType frameCountType = FrameCountType.fps;

    private void Awake()
    {
        text = gameObject.GetComponent<Text>();
    }
    void Start()
    {
        startText = text.text;
        StartCoroutine(UpdateFPSCount());
    }

    void Update()
    {
        numberOfFrames++;
        
    }

    IEnumerator UpdateFPSCount()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1f);
            text.text = startText + " " + numberOfFrames.ToString();
            numberOfFrames = 0;
        }

    }

    private void OnGUI()
    {
        
    }
}
