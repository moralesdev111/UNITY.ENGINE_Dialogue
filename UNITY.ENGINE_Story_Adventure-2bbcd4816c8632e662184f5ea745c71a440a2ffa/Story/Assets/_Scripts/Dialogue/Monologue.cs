using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monologue : MonoBehaviour
{
	[SerializeField] private DialogueUI dialogueUI;
	[SerializeField] private DialogueBubbles dialogueBubbles;

	// Start is called before the first frame update
	void Start()
	{
		dialogueUI.PlayerConversant.onMonologue += HandleMonologue;
	}

	private void HandleMonologue()
	{
		if(dialogueUI && dialogueBubbles != null)
		{
			dialogueBubbles.SetPlayerDialogueBubbleOffsetAccordingToPosition();
			dialogueUI.ActualDialogue2dGameobject.gameObject.SetActive(true);
			dialogueBubbles.PlayerBubble.gameObject.SetActive(true);
			dialogueBubbles.NpcBubble.gameObject.SetActive(false);
			dialogueUI.PlayerBubbleText.text = dialogueUI.PlayerConversant.GetText();
			StartCoroutine(MonologueTimer());
		}
	}

	private IEnumerator MonologueTimer()
	{
		yield return new WaitForSeconds(dialogueUI.PlayerConversant.BubbleSpeakingDuration);
		dialogueUI.PlayerConversant.SetPlayerIsSpeaking(false);
		dialogueBubbles.PlayerBubble.gameObject.SetActive(false);
		dialogueUI.ActualDialogue2dGameobject.gameObject.SetActive(true); ;
		dialogueUI.PlayerConversant.QuitDialogue();
	}
}