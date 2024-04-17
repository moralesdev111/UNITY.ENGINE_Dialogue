using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, playerIInteracteable
{
	public Actions[] toDoActions;
	[SerializeField] private float stoppingDistance = 1.0f;
	private NPCWaitForPlayerArrival NPCWaitForPlayerArrival;

	private void Start()
	{
		NPCWaitForPlayerArrival = GetComponent<NPCWaitForPlayerArrival>();
		NPCWaitForPlayerArrival.Npc = this;
	}

	public Vector3 NPCInteractionPosition()
	{
		return transform.position + transform.forward * stoppingDistance;
	}

	public void Interact(Player player)
	{
		StartCoroutine(NPCWaitForPlayerArrival.WaitForPlayerArrival(player));
	}
}
