using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLook : MonoBehaviour
{

    [SerializeField]
    private Transform target;
    [SerializeField]
    [Range(0,1)]
    private float weight;

    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

  void FixedUpdate()
    {

    }

    void OnAnimatorIK()
    {
        if(anim.enabled)
        {
            if(target!=null)
            {
                if (weight == 0f)
                {
                    return;
                }
                else if(weight>0f)
                {
                    Debug.Log("im looking");
                    anim.SetLookAtPosition(target.position);            //  look at object 
                    anim.SetLookAtWeight(weight, 0f, 1f, 0.5f);         // set values for each part 
                }
            }
        }
    }
}
