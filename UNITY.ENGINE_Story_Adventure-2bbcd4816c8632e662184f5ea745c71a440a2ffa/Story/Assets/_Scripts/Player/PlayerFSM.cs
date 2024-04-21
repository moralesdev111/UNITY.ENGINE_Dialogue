using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : MonoBehaviour
{
	[SerializeField] private PlayerStates state;
	public PlayerStates State { get { return state; } }
	[SerializeField] private Player player;
	public enum PlayerStates
	{
		Idle,
		Movement,
		Dialogue
	}

	// Start is called before the first frame update
	void Start()
	{
		state = PlayerStates.Idle;
	}

	// Update is called once per frame
	void Update()
	{
		FSMConditions();
		FSMActions(state);
	}

	private void FSMConditions()
	{
		if (DataManager.Instance.DialogueCurrentParticipants.PlayerConversant.CurrentDialogue != null)
		{
			state = PlayerStates.Dialogue;
		}
		else if (player.Agent.velocity.magnitude > 0.1f)
		{
			state = PlayerStates.Movement;
		}
		else
		{
			state = PlayerStates.Idle;
		}
	}

	public void FSMActions(PlayerStates newState)
	{
		state = newState;

		switch (state)
		{
			case PlayerStates.Idle:
				//idle actions
				break;

			case PlayerStates.Movement:
				// movement actions
				break;

			case PlayerStates.Dialogue:
				// dialogue actions
				break;
		}
	}
}