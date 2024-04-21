using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
	public Actions[] toDoAutoActions;
	public Actions[] dialogueTriggerActions;
	[SerializeField] private float stoppingDistance = 1.0f;
	private WaitPlayerForInteract WaitPlayerForInteract;

	private void Start()
	{
		WaitPlayerForInteract = GetComponent<WaitPlayerForInteract>();
		WaitPlayerForInteract.Interact = this;
	}

	public Vector3 NPCInteractionPosition()
	{
		return transform.position + transform.forward * stoppingDistance;
	}

	public void Interaction(Player player)
	{
		StartCoroutine(WaitPlayerForInteract.WaitForPlayerArrival(player));
	}
}
