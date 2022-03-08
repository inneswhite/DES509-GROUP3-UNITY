using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCamera : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) > 0.5f)
        {
            Vector3 direction = target.position - target.position;
            direction.y = 0;
            Vector3 newrotation = Vector3.RotateTowards(transform.position, direction, speed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(newrotation);
            transform.LookAt(target);
        }
    }
}
