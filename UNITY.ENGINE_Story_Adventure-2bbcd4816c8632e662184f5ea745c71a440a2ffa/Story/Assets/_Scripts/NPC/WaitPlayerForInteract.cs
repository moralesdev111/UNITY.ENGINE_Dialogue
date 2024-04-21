using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitPlayerForInteract : MonoBehaviour
{
	private Interact interact;
	public Interact Interact { set { interact = value; } }


	public IEnumerator WaitForPlayerArrival(Player player)
	{
		while (!player.PlayerMovement.CheckIfPlayerArrived())
		{
			yield return null;
		}
		// when arriving the code below will run

		player.PlayerMovement.SetFacingDirection(transform.position);
		ExecuteActions();
	}

	private void ExecuteActions()
	{
		if (interact.toDoAutoActions != null)
		{
			for (int i = 0; i < interact.toDoAutoActions.Length; i++)
			{
				interact.toDoAutoActions[i].Act();
				if (interact.dialogueTriggerActions != null)
				{
					for (int j = 0; j < interact.dialogueTriggerActions.Length; j++)
					{
						//npc.dialogueTriggerActions[i].Act();
					}
				}
			}
		}
	}
}
