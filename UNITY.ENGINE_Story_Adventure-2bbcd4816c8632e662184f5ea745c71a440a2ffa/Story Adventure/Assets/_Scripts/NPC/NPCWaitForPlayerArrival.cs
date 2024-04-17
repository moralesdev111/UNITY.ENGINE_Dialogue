using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWaitForPlayerArrival : MonoBehaviour
{
	private NPC npc;
	public NPC Npc { set { npc = value; } }


	public IEnumerator WaitForPlayerArrival(Player player)
	{
		while (!player.PlayerMovement.CheckIfPlayerArrived())
		{
			yield return null;
		}
		// when arriving the code below will run

		player.PlayerMovement.SetFacingDirection(transform.position);

		for (int i = 0; i < npc.toDoActions.Length; i++)
		{
			npc.toDoActions[i].Act();
		}
	}
}
