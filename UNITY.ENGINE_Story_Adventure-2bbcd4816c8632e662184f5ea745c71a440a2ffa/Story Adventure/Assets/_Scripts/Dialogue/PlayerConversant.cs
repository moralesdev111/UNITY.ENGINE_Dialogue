using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PlayerConversant : MonoBehaviour
{
	[SerializeField] private float bubbleSpeakingDuration = 2.5f;
	public float BubbleSpeakingDuration { get { return bubbleSpeakingDuration; } }
	private Dialogue currentDialogue;
	public Dialogue CurrentDialogue { get { return currentDialogue; } }
	private DialogueNode currentNode;
	public DialogueNode CurrentNode { get { return currentNode; } }
	private bool playerIsChoosing = false;
	private bool playerIsSpeaking = false;
	public bool PlayerIsSpeaking { get { return playerIsSpeaking; } }
	public void SetPlayerIsSpeaking(bool speaking) => playerIsSpeaking = speaking;
	public event Action onDialogueEvent;
	public event Action onNewDialogue;
	public event Action onMonologue;
	public event Action onDialogueFinish;

	public void StartDialogue(Dialogue newdialogue)
	{
		currentDialogue = newdialogue;
		currentNode = currentDialogue.GetRootNode();
		if (currentDialogue.IsMonologue)
		{
			Monologue();
			return;
		}
		onDialogueEvent();
		onNewDialogue();
	}

	public void QuitDialogue()
	{
		currentDialogue = null;
		currentNode = null;
		playerIsChoosing = false;
		onDialogueEvent();
		onDialogueFinish();
	}

	public void Monologue()
	{
		if (onMonologue != null)
		{
			onMonologue();
		}
	}

	public bool DialogueIsActive()
	{
		return currentDialogue != null;
	}

	public bool GetPlayerIsChoosing()
	{
		return playerIsChoosing;
	}

	public bool GetPlayerIsSpeaking()
	{
		return playerIsSpeaking;
	}
	public string GetText()
	{
		if (currentNode == null)
		{
			return "ERROR RETRIEVING TEXT";
		}
		return currentNode.GetText();
	}

	public IEnumerable<DialogueNode> GetDialogueChoices()
	{
		return currentDialogue.GetPlayerChildren(currentNode);
	}

	public void SelectDialogueChoice(DialogueNode chosenNode)
	{
		currentNode = chosenNode;
		playerIsChoosing = false;
		playerIsSpeaking = true;
		NextButtonFunctionality();
	}

	public void NextButtonFunctionality()
	{
		if (currentDialogue != null)
		{
			if (playerIsSpeaking)     // speaking
			{
				StartCoroutine(PlayerSpeak());
				return;
			}
			if (currentNode.ReverseDialogueToStartAfterNode)   // going back to dialogue start choices
			{
				HandleStartingDialogueAgain();
				return;
			}
			int numberOfPlayerResponese = currentDialogue.GetPlayerChildren(currentNode).Count();
			if (numberOfPlayerResponese > 0)      // choosing ui
			{
				playerIsChoosing = true;
				onDialogueEvent();
				return;
			}
			HandleIfNPCNodes(); // npc turn
			onDialogueEvent();
		}
	}

	private void HandleIfNPCNodes()
	{
		DialogueNode[] currentDialogueNPCChildrenNodes = currentDialogue.GetNPCChildren(currentNode).ToArray();
		if (currentDialogueNPCChildrenNodes.Length > 0)
		{
			currentNode = currentDialogueNPCChildrenNodes[0];
		}
		else
		{
			currentDialogue = null;
			currentNode = null;
		}
	}

	private void HandleStartingDialogueAgain()
	{
		DialogueNode rootNode = currentDialogue.GetRootNode();
		currentNode = rootNode;
		playerIsChoosing = true;
		onDialogueEvent();
		return;
	}

	public bool DialogueHasNextNode()
	{
		return currentDialogue.GetAllChildren(currentNode).ToArray().Count() > 0;
	}

	private IEnumerator PlayerSpeak()
	{
		playerIsSpeaking = true;
		onDialogueEvent();
		yield return new WaitForSeconds(bubbleSpeakingDuration);
		playerIsSpeaking = false;
		NextButtonFunctionality();
		onDialogueEvent();
	}

	public IEnumerator NPCSpeak()
	{
		yield return new WaitForSeconds(bubbleSpeakingDuration);
		NextButtonFunctionality();
	}
}