using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLook : MonoBehaviour
{
   
    [SerializeField]
    private Transform Target;

    [SerializeField]
    private Transform TargetObject;

    [SerializeField]
    private float radius;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitcols = Physics.OverlapSphere(transform.position, radius);
        foreach(var hitcol in hitcols)
        {
           if(hitcol.CompareTag("Player"))
            {
                transform.LookAt(Target);
            }
           else
            {
                transform.LookAt(TargetObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
