using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using System;

public class DialogueUI : MonoBehaviour
{
	[SerializeField] private PlayerConversant playerConversant;
	public PlayerConversant PlayerConversant { get { return playerConversant; } }
	[SerializeField] private Transform playerBody;
	public Transform PlayerBody { get { return playerBody; } }

	[SerializeField] private TextMeshProUGUI playerBubbleText;
	public TextMeshProUGUI PlayerBubbleText { get { return playerBubbleText; } }
	[SerializeField] private Transform playerChoicesParent;
	[SerializeField] private GameObject playerChoicePrefab;
	[SerializeField] private TextMeshProUGUI npcText;
	[SerializeField] private GameObject actualDialogue2dGameobject;
	public GameObject ActualDialogue2dGameobject { get { return  actualDialogue2dGameobject; } }

	private DialogueBubbles dialogueBubbles;

	private void Start()
	{
		dialogueBubbles = GetComponent<DialogueBubbles>();
		if (playerConversant != null)
		{
			SetupListeners();
			UpdateDialogueUI();
		}
	}

	private void UpdateDialogueUI()
	{
		actualDialogue2dGameobject.SetActive(playerConversant.DialogueIsActive());
		if (!playerConversant.DialogueIsActive()) return;
		dialogueBubbles.HandleBubbles();
		playerChoicesParent.gameObject.SetActive(playerConversant.GetPlayerIsChoosing());
		if(playerChoicesParent.gameObject.activeInHierarchy)
		{
			DataManager.Instance.Inventory.InventoryUI.InventorybackgroundUI.gameObject.SetActive(false);
		}
		PlayerChoosingOrNPCSpeaks();
	}

	private void PlayerChoosingOrNPCSpeaks()
	{
		if (playerConversant.GetPlayerIsChoosing()) // player speaks
		{
			HandlePlayerChoosingButtons();
		}
		else  // npc speaks
		{
			npcText.text = playerConversant.GetText();
			StartCoroutine(playerConversant.NPCSpeak());
		}
	}

	private void HandlePlayerChoosingButtons()
	{
		CleanDanglingChilds();
		PopulateChildrenAccordingToDialogueNodes();
	}

	private void PopulateChildrenAccordingToDialogueNodes()
	{
		foreach (DialogueNode playerChoice in playerConversant.GetDialogueChoices())
		{
			GameObject choiceInstance = Instantiate(playerChoicePrefab, playerChoicesParent);
			var buttonText = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
			buttonText.text = playerChoice.GetText();
			Button button = choiceInstance.GetComponentInChildren<Button>();
			button.onClick.AddListener(() =>
			{
				playerConversant.SelectDialogueChoice(playerChoice);
			});
		}
	}

	private void CleanDanglingChilds()
	{
		for (int i = 0; i < playerChoicesParent.childCount; i++)
		{
			Transform childTransform = playerChoicesParent.GetChild(i);
			Destroy(childTransform.gameObject);
		}
	}

	private void SetupListeners()
	{
		playerConversant.onDialogueEvent += UpdateDialogueUI;
	}
}