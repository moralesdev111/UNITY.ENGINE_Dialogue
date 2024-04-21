using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
	public static DataManager Instance { get; private set; }

	[SerializeField] private Inventory inventory;
	public Inventory Inventory { get { return inventory; } }
	public void SetInventory (Inventory inventory) {this.inventory = inventory; }
	[SerializeField] private DialogueCurrentParticipants dialogueCurrentParticipants;
	public DialogueCurrentParticipants DialogueCurrentParticipants { get { return dialogueCurrentParticipants; } }
	public void SetDialogueCurrentParticipants(DialogueCurrentParticipants dialogueCurrentParticipants) { this.dialogueCurrentParticipants = dialogueCurrentParticipants; }

	public string previousSceneName {  get; private set; }
	public SceneManager SceneManager { get; private set; }


	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		SceneManager = GetComponentInChildren<SceneManager>();
	}

	public void SetPreviousSceneName(string name)
	{
		previousSceneName = name;
	}
}
