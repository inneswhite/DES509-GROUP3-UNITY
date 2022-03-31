﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
	private CharacterController charController;
	private CollisionFlags collisionFlags = CollisionFlags.None;
	[SerializeField]
	private PlayerCop playercop;
	[SerializeField]
	private float moveSpeed = 1f;
	public bool isMove;
	public bool finished_Movement;
	private Vector3 targetposition = Vector3.zero;
	private Vector3 player_Move = Vector3.zero;
	private float playerPointDistance;
	LayerMask layermask = 1 << 8;

	private float gravity = 9.8f;
	private float height;
	private float dist;

	private Camera Maincam;
	private Camera Sidecam, Sidecam2, Sidecam3;
	[SerializeField]
	private Camera thiscam;


	[SerializeField]
	private GameObject groundpointer;
	private GameObject mousepointholder;

	private Inventory inventory;


	


	void Awake()
	{
		charController = GetComponent<CharacterController>();
	}
	private void Start()
	{
		//check for all cameras
		Maincam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		Sidecam = GameObject.FindGameObjectWithTag("SideCamera").GetComponent<Camera>();
		Sidecam2 = GameObject.FindGameObjectWithTag("SideCamera2").GetComponent<Camera>();
		Sidecam3 = GameObject.FindGameObjectWithTag("SideCamera3").GetComponent<Camera>();

		thiscam = Maincam;
	}

	void Update()
	{
		CameraSwitch();
		CalculateHeight();
	    CheckIfFinishedMovement();
		MakePointer();


	}
	bool IsGrounded()
	{
		return collisionFlags == CollisionFlags.CollidedBelow ? true : false;
	}

	void CalculateHeight()
	{
		if (IsGrounded())
		{
			height = 0f;
		}
		else
		{
			height -= gravity * Time.deltaTime;
		}
	}

	void CheckIfFinishedMovement()
	{
		// if  movement isnt finished
		if (!finished_Movement)
		{
			MoveThePlayer();

			player_Move.y = height * Time.deltaTime;
			collisionFlags = charController.Move(player_Move);
		}


	}


	void MoveThePlayer()
	{
		if(playercop.currentState==PlayerCop.playerState.idle)
		{
			if (Input.GetMouseButtonDown(0))		
			{
				RaycastHit hit;
				Ray ray = thiscam.ScreenPointToRay(Input.mousePosition);		// mouse position after point click

				if (Physics.Raycast(ray, out hit,layermask))
				{
					if (hit.collider.CompareTag("Ground") || hit.collider.CompareTag("CameraTrigger"))
					{					
						playerPointDistance = Vector3.Distance(transform.position, hit.point);
						if (playerPointDistance > 0f)
						{
							isMove = true;
							Debug.Log(hit.point);
							targetposition = hit.point;
						}
					}
				}
			}

			if (isMove)
			{
				Vector3 targetTemp = new Vector3(targetposition.x, transform.position.y, targetposition.z);
				dist = Vector3.Distance(transform.position, targetTemp); //calculate distance of player position and temporary position
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetTemp - transform.position), 15.0f * Time.deltaTime);
				player_Move = transform.forward * moveSpeed * Time.deltaTime;

				if (dist < 0.1f)
				{
					isMove = false;
				}
			}
			else
			{
				player_Move.Set(0f, 0f, 0f);
			}
		}
	}

	void MakePointer()
	{
		if (playercop.currentState == PlayerCop.playerState.idle)
		{
			if (Input.GetMouseButtonUp(0))          // mouse click
			{
				RaycastHit mousehit;
				Ray mouseray = thiscam.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(mouseray, out mousehit,layermask))
				{
					if (mousehit.collider.CompareTag("Ground") || mousehit.collider.CompareTag("CameraTrigger"))
					{
						Vector3 groundposition = mousehit.point;        // set groundpointer position to mouse click position
						groundposition.y = 0.35f;
						if (mousepointholder == null)
						{
							mousepointholder = Instantiate(groundpointer) as GameObject;
							mousepointholder.transform.position = groundposition;
						}
						else
						{
							Destroy(mousepointholder);
							mousepointholder = Instantiate(groundpointer) as GameObject;
							mousepointholder.transform.position = groundposition;
						}
					}
				}
			}
		}
	}


	public void CameraSwitch()
	{
		if (Sidecam.enabled)
		{
			thiscam = Sidecam;
			Maincam.enabled = false;
			Sidecam2.enabled = false;
			Sidecam3.enabled = false;

		}
			if (Sidecam2.enabled)
			{
				thiscam = Sidecam2;
			Maincam.enabled = false;
			Sidecam.enabled = false;
			Sidecam3.enabled = false;
			}
			if (Sidecam3.enabled)
			{
				thiscam = Sidecam3;
			Maincam.enabled = false;
			Sidecam.enabled = false;
			Sidecam2.enabled = false;
			}
			else if (Maincam.enabled)
			{
				thiscam = Maincam;
			}
		}

	void OnApplicationQuit()
	{
		playercop.currentState = PlayerCop.playerState.idle;
	}
}


		










































