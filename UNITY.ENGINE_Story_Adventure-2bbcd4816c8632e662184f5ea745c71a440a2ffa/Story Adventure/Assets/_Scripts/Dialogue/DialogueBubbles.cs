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
		dialogueUI.PlayerConversant.onNewDialogue += CoroutineNPCVariableSetting;
		dialogueUI.PlayerConversant.onNewDialogue += SetPlayerDialogueBubbleOffsetAccordingToPosition;
	}

	public void HandleBubbles()
	{
		npcBubble.SetActive(!dialogueUI.PlayerConversant.GetPlayerIsChoosing() && !dialogueUI.PlayerConversant.GetPlayerIsSpeaking());

		playerBubble.SetActive(dialogueUI.PlayerConversant.GetPlayerIsSpeaking());

		dialogueUI.PlayerBubbleText.text = dialogueUI.PlayerConversant.GetText();
	}
	public void CoroutineNPCVariableSetting()
	{
		StartCoroutine(WaitForActiveNPCToBeSet());
	}
	public IEnumerator WaitForActiveNPCToBeSet()
	{
		while(DataManager.Instance.DialogueCurrentParticipants.ActiveNPCSpeaker == null)
		{
			yield return null;
		}
		SetNPCDialogueBubbleOffsetAccordingToPosition();
	}
	public void SetNPCDialogueBubbleOffsetAccordingToPosition()
	{
			Vector3 npcScreenPosition = Camera.main.WorldToScreenPoint(DataManager.Instance.DialogueCurrentParticipants.ActiveNPCSpeaker.transform.position + new Vector3(0.5f, 1.5f, 0f));
			npcBubble.transform.position = npcScreenPosition;
	}
		
	public void SetPlayerDialogueBubbleOffsetAccordingToPosition()
	{
		if (dialogueUI.PlayerBody != null)
		{
			Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(dialogueUI.PlayerBody.transform.position + new Vector3(-3.0f, 3.0f, 0f));
			playerBubble.transform.position = playerScreenPosition;
		}
	}
}