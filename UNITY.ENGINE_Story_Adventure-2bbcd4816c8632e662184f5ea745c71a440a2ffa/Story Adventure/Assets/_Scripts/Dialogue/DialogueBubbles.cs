using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBubbles : MonoBehaviour
{
	private DialogueUI dialogueUI;
	[SerializeField] private GameObject npcBubble;
	public GameObject NpcBubble { get { return npcBubble; } }
	[SerializeField] private GameObject playerBubble;
	public GameObject PlayerBubble { get { return playerBubble; } }

	private void Start()
	{
		dialogueUI = GetComponent<DialogueUI>();
		dialogueUI.PlayerConversant.onNewDialogue += SetNPCDialogueBubbleOffsetAccordingToPosition;
		dialogueUI.PlayerConversant.onNewDialogue += SetPlayerDialogueBubbleOffsetAccordingToPosition;
	}

	public void HandleBubbles()
	{
		npcBubble.gameObject.SetActive(!dialogueUI.PlayerConversant.GetPlayerIsChoosing() && !dialogueUI.PlayerConversant.GetPlayerIsSpeaking());

		playerBubble.gameObject.SetActive(dialogueUI.PlayerConversant.GetPlayerIsSpeaking());

		dialogueUI.PlayerBubbleText.text = dialogueUI.PlayerConversant.GetText();
	}

	public void SetNPCDialogueBubbleOffsetAccordingToPosition()
	{
		if (DataManager.Instance.DialogueCurrentParticipants.ActiveNPCSpeaker != null)
		{
			Vector3 npcScreenPosition = Camera.main.WorldToScreenPoint(DataManager.Instance.DialogueCurrentParticipants.ActiveNPCSpeaker.transform.position + new Vector3(-3.0f, 2f, 0f));
			npcBubble.transform.position = npcScreenPosition;
		}
	}
	public void SetPlayerDialogueBubbleOffsetAccordingToPosition()
	{
		if (dialogueUI.PlayerBody != null)
		{
			Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(dialogueUI.PlayerBody.transform.position + new Vector3(-2.5f, 2.0f, 0f));
			playerBubble.transform.position = playerScreenPosition;
		}
	}
}