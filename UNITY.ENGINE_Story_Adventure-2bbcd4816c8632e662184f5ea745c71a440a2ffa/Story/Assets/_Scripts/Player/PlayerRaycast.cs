using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
	private Player player;
	public Player Player { set { player = value; } }


	public void RaycastHandling()
	{
		Ray cameraToScreen = player.MainCamera.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(cameraToScreen, out RaycastHit hit, Mathf.Infinity))
		{
			if (hit.collider)
			{
				GetNPCAndMoveToPosition(hit);
			}
		}
	}

	private void GetNPCAndMoveToPosition(RaycastHit hit)
	{
		Interact interactable = hit.collider.GetComponent<Interact>();
		if (interactable)
		{
			player.PlayerMovement.MovePlayerToPosition(interactable.NPCInteractionPosition());
			interactable.Interaction(player);
		}
		else
		{
			player.PlayerMovement.MovePlayerToPosition(hit.point);
		}
	}
}
