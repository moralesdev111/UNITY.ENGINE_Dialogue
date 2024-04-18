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

		if (npc.toDoAutoActions != null)
		{
			for (int i = 0; i < npc.toDoAutoActions.Length; i++)
			{
				npc.toDoAutoActions[i].Act();
				if (npc.dialogueTriggerActions != null)
				{
					for (int j = 0; j < npc.dialogueTriggerActions.Length; j++)
					{
						//npc.dialogueTriggerActions[i].Act();
					}
				}
			}
		}
	}
}
