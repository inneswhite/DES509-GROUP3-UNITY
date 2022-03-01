using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMove : MonoBehaviour
{
	private CharacterController charController;
	private CollisionFlags collisionFlags = CollisionFlags.None;
	private float moveSpeed = 1f;
	public bool canMove;
	public bool finished_Movement;
	private Vector3 target_Pos = Vector3.zero;
	private Vector3 player_Move = Vector3.zero;
	private float player_ToPointDistance;

	private float gravity = 9.8f;
	private float height;
	private Camera Maincam;
	private Camera Sidecam, Sidecam2, Sidecam3;
	[SerializeField]
	private Camera thiscam;


	void Awake()
	{
		charController = GetComponent<CharacterController>();
	}
	private void Start()
	{
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
		//CheckIfFinishedMovement();
		MovePlayerWithoutRotation();
	
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

	void MovePlayerWithoutRotation()
    {
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = thiscam.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit))
				{
					target_Pos = hit.point;
				}
			}
		charController = GetComponent(typeof(CharacterController)) as CharacterController;
		if (Vector3.Magnitude(target_Pos - transform.position) > 0.1f)
		{
			if (charController.isGrounded)
			{
				player_Move = target_Pos - transform.position;
				player_Move = transform.TransformDirection(player_Move);
				player_Move*= moveSpeed;
			}
			player_Move.y -= gravity * Time.deltaTime; 
			charController.Move(player_Move * Time.deltaTime);
		}
		else
		{
			transform.position = target_Pos;
		}
	}

	void MoveThePlayer()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit))
			{
				if (hit.collider is MeshCollider)
				{
					player_ToPointDistance = Vector3.Distance(transform.position, hit.point);
					if (player_ToPointDistance >= 1.0f)
					{
						canMove = true;
						target_Pos = hit.point;
					}
				}
			}
		} 

		if (canMove == true)
		{
			Vector3 targetTemp = new Vector3(target_Pos.x, transform.position.y, target_Pos.z);
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetTemp - transform.position), 15.0f * Time.deltaTime);
			player_Move = transform.forward * moveSpeed * Time.deltaTime;

			if (Vector3.Distance(transform.position, target_Pos) <= 1f)
			{
				canMove = false;
			}
		}
		else
		{
			player_Move.Set(0f, 0f, 0f);
		}
	}

	public void CameraSwitch()
    {
		if (Sidecam.enabled || Maincam.enabled == false)
		{
			thiscam = Sidecam;
		}
		if (Sidecam2.enabled)
		{
			thiscam = Sidecam2;
		}
		if (Sidecam3.enabled)
		{
			thiscam = Sidecam3;
		}
		else if (Maincam.enabled)
		{
			thiscam = Maincam;
		}
	}
}


		










































