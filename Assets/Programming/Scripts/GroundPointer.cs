using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPointer : MonoBehaviour
{
    private Transform player;
    public float dist;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, player.position);
        if (dist<=2f)
        {
            Debug.Log("hi im working");
            Destroy(gameObject);
        }
    }
}
