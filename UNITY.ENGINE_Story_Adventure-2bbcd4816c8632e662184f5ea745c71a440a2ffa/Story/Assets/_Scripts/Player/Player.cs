using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
	public Camera MainCamera { get { return mainCamera; } }
	public NavMeshAgent Agent { get { return agent; } }
	public PlayerMovement PlayerMovement { get { return playerMovement; } }

	private NavMeshAgent agent;
	private PlayerMovement playerMovement;
	private PlayerRaycast playerRaycast;
	private Camera mainCamera;
	

	[SerializeField] private float TurnAngleSpeed = 15.0f;


	// Start is called before the first frame update
	void Start()
	{
		playerMovement = GetComponent<PlayerMovement>();
		playerRaycast = GetComponent<PlayerRaycast>();
		playerRaycast.Player = this;
		playerMovement.Player = this;
		agent = GetComponent<NavMeshAgent>();

		mainCamera = Camera.main;

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0) && !ExtensionLibrary.IsMouseOverUI() && DataManager.Instance.DialogueCurrentParticipants.PlayerConversant.CurrentDialogue == null)
		{
			//&& !Dialogue.Instance.DialoguePanel.activeInHierarchy
			OnClick();
		}
		if (playerMovement.Turning && transform.rotation != playerMovement.TargetRotation)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, playerMovement.TargetRotation, TurnAngleSpeed * Time.deltaTime);
		}
	}

	void OnClick()
	{
		playerRaycast.RaycastHandling();
	}
}