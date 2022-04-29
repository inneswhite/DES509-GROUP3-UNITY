using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLook : MonoBehaviour
{
    Vector3 targetpos;
    float angle;
    [SerializeField]
    private Transform Target;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        targetpos = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        angle = Vector3.SignedAngle(targetpos,transform.forward,Vector3.up);
        transform.Rotate(new Vector3(0, 0, angle));
    }
}
