using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    #region
    [SerializeField] private Transform target;
    [SerializeField] private float smoothtime;
    private Vector3 currentvelocity = Vector3.zero;
    private Vector3 Offset;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
         Offset = transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 relativePosition = target.position + Offset;  
        transform.position = Vector3.SmoothDamp(transform.position,relativePosition, ref currentvelocity, smoothtime);
    }
}
