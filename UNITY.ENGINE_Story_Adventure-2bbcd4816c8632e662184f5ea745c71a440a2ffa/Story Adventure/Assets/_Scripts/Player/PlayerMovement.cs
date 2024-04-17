using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private Player player; 
	public Player Player { set { player = value; } }
	private bool turning = false;
	public bool Turning { get { return turning; } }
	private Quaternion targetRotation;
	public Quaternion TargetRotation { get { return targetRotation; } }


	public bool CheckIfPlayerArrived()
	{
		return !player.Agent.pathPending && player.Agent.remainingDistance <= player.Agent.stoppingDistance;
	}

	public void MovePlayerToPosition(Vector3 targetPosition)
	{
		player.PlayerMovement.turning = false;
		player.Agent.SetDestination(targetPosition);
	}

	public void SetFacingDirection(Vector3 targetDirection)
	{
		turning = true;
		targetRotation = Quaternion.LookRotation(targetDirection - transform.position);
	}
}
