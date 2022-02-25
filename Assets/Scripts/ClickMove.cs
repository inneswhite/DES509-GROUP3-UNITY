using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMove : MonoBehaviour
{
    private CharacterController controller;
    private float speed = 1;
    private float gravity = 20.0F;
    private Vector3 targetPosition;
    private Vector3 moveDirection;
    public Transform target;
    public int switchvalue;
    private Camera Maincam;
    private Camera Sidecam;
    [SerializeField]
    private Camera  thiscam;

    private void Start()
    {
        Maincam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Sidecam = GameObject.FindGameObjectWithTag("SideCamera").GetComponent<Camera>();
    }
    void Update()
    {
        if(Sidecam.enabled)
        {
            thiscam = Sidecam;
        }
        else
        {
            thiscam = Maincam;
        }
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = thiscam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    targetPosition = hit.point;
                }
            }
        


        controller = GetComponent(typeof(CharacterController)) as CharacterController;
        if (Vector3.Magnitude(targetPosition - transform.position) > 0.1f)
        {
            if (controller.isGrounded)
            {
                moveDirection = targetPosition - transform.position;
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;
                if (moveDirection != Vector3.zero)
                {
                    Quaternion rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                    Quaternion.RotateTowards(transform.rotation, rotation, 360 * Time.deltaTime);
                }
            }
            
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection* Time.deltaTime);
            
        }
        else
        {
            transform.position = targetPosition;
        }
    }
}
