using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLook : MonoBehaviour
{

    [SerializeField]
    private Transform target;

    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

  

    void OnAnimatorIK()
    {
        if(anim.enabled)
        {
            if(target!=null)
            {
                anim.SetLookAtPosition(target.position);
                anim.SetLookAtWeight(1.0f, 0.5f, 0.5f, 0f);
            }
        }
    }
}
