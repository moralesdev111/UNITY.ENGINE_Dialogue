using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;


public class DialogueUI : MonoBehaviour
	{
		[SerializeField] private PlayerConversant playerConversant;
		[SerializeField] private Transform playerBody;
		[SerializeField] private TextMeshProUGUI npcText;
		[SerializeField] private Button nextButton;
		[SerializeField] private Button quitButton;
		[SerializeField] private GameObject npcBubble;
		[SerializeField] private GameObject playerBubble;
		[SerializeField] private TextMeshProUGUI playerBubbleText;
		[SerializeField] private Transform playerChoicesParent;
		[SerializeField] private GameObject playerChoicePrefab;
		[SerializeField] private GameObject activeNPConversant;

		private void Start()
		{
			if(playerConversant != null)
			{
				SetupListeners();
				UpdateDialogueUI();
			}
		}
		
		private void UpdateDialogueUI()
		{
			gameObject.SetActive(playerConversant.DialogueIsActive());
			if (!playerConversant.DialogueIsActive()) return;
			HandleBubbles();
			playerChoicesParent.gameObject.SetActive(playerConversant.GetPlayerIsChoosing());
			PlayerChoosingOrNPCSpeaks();
		}

		private void PlayerChoosingOrNPCSpeaks()
		{
			if (playerConversant.GetPlayerIsChoosing())
			{
				HandlePlayerChoosingButtons();
			}
			else
			{
				npcText.text = playerConversant.GetText();
				if (!playerConversant.CurrentNode.ReverseDialogueToStartAfterNode)
				{
					nextButton.gameObject.SetActive(playerConversant.DialogueHasNextNode());
					return;
				}
				nextButton.gameObject.SetActive(true);
			}
		}

		private void HandleBubbles()
		{
			npcBubble.SetActive(!playerConversant.GetPlayerIsChoosing() && !playerConversant.GetPlayerIsSpeaking());
			
			playerBubble.gameObject.SetActive(playerConversant.GetPlayerIsSpeaking());
			
			playerBubbleText.text = playerConversant.GetText();
		}

		private void SetNPCDialogueBubbleOffsetAccordingToPosition()
		{
			if (DataManager.Instance.DialogueCurrentParticipants.ActiveNPCSpeaker != null)
			{
				Vector3 npcScreenPosition = Camera.main.WorldToScreenPoint(DataManager.Instance.DialogueCurrentParticipants.ActiveNPCSpeaker.transform.position + new Vector3(0.5f, 0.5f, 0f));
				npcBubble.transform.position = npcScreenPosition;
			}
		}
		private void SetPlayerDialogueBubbleOffsetAccordingToPosition()
		{
			if(playerBody != null)
			{
				Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(playerBody.transform.position + new Vector3(-1.5f,-1.5f,0f));
				playerBubble.transform.position = playerScreenPosition;
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
			playerConversant.onNewDialogue += SetPlayerDialogueBubbleOffsetAccordingToPosition;
			playerConversant.onNewDialogue += SetNPCDialogueBubbleOffsetAccordingToPosition;
			nextButton.onClick.AddListener(playerConversant.NextButtonFunctionality);
			quitButton.onClick.AddListener(playerConversant.QuitDialogue);
		}
	}
