using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
	private CharacterController charController;
	private CollisionFlags collisionFlags = CollisionFlags.None;
	[SerializeField]
	private PlayerCop playercop;
	[SerializeField]
	private float moveSpeed = 1f;
	private bool isMove;
	private bool finished_Movement;
	private Vector3 targetposition = Vector3.zero;
	private Vector3 player_Move = Vector3.zero;
	private float playerPointDistance;
	LayerMask layermask = 1 << 8;

	private float gravity = 9.8f;
	private float height;
	private float dist;

	private Camera Maincam;

	[Header("Side Cameras")]
	[SerializeField]
	private Camera Sidecam;
	[SerializeField]
	private Camera Sidecam2;
    [SerializeField]
    private Camera InsideCam;
	[Header("Reference Cam")]
	[SerializeField]
	private Camera thiscam;


	[SerializeField]
	private GameObject groundpointer;
	private GameObject mousepointholder;

	private Inventory inventory;

	[HideInInspector]
	public bool istalking;


	


	void Awake()
	{
		charController = GetComponent<CharacterController>();
	}
	private void Start()
	{
		//check for all cameras
		Maincam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

		thiscam = Maincam;
	}

	void Update()
	{
		if (!istalking)
		{
            if(EventSystem.current.IsPointerOverGameObject())           // if mouse cursor is over ui button
            {
                return;
            }
			CameraSwitch();
			CalculateHeight();
			CheckIfFinishedMovement();
			MakePointer();
		}
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
			if (Input.GetMouseButtonDown(0))		
			{
				RaycastHit hit;
				Ray ray = thiscam.ScreenPointToRay(Input.mousePosition);		// mouse position after point click

				if (Physics.Raycast(ray, out hit,layermask))
				{
					if (hit.collider.CompareTag("Ground") || hit.collider.CompareTag("CameraTrigger"))  // mouse pos raycast hit
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
                //IMPLEMENT FOOTSTEPS AUDIO

				if (dist < 0.3f)
				{
					isMove = false;
                    // STOP AUDIO 
				}
			}
			else
			{
				player_Move.Set(0f, 0f, 0f);
			}
		}	

	void MakePointer()
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


	public void CameraSwitch()
	{
		if (Sidecam.enabled)
		{
			thiscam = Sidecam;
			Maincam.enabled = false;
			Sidecam2.enabled = false;
		}
			if (Sidecam2.enabled)
			{
				thiscam = Sidecam2;
			Maincam.enabled = false;
			Sidecam.enabled = false;
			}
			else if (Maincam.enabled)
			{
				thiscam = Maincam;
			}
            else if(InsideCam.enabled)
             {
                 thiscam = InsideCam;

        }
		}
}


		










































