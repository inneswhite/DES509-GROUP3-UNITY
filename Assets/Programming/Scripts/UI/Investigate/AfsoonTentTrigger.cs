using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfsoonTentTrigger : MonoBehaviour
{
    BoxCollider boxCollider;
    [SerializeField] UIInvestigateButton uIInvestigateButton;

    private void Awake()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            uIInvestigateButton.Activate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            uIInvestigateButton.Deactivate();
        }
    }
}
