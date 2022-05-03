using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideCamTrigger : MonoBehaviour
{
    [SerializeField]
    private Camera sidecam;
    [SerializeField]
    private Camera insidecam;
    // Start is called before the first frame update
    void Start()
    {
        insidecam.enabled = false;
    }

   
    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            insidecam.enabled = true;
            sidecam.enabled = false;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            insidecam.enabled = false;
            sidecam.enabled = true;
        }
    }
}
