using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
	public static DataManager Instance { get; private set; }

	[SerializeField] private Inventory inventory;
	public Inventory Inventory { get { return inventory; } }
	[SerializeField] private DialogueCurrentParticipants dialogueCurrentParticipants;
	public DialogueCurrentParticipants DialogueCurrentParticipants { get { return dialogueCurrentParticipants; } }

	public string previousSceneName {  get; private set; }
	public SceneManager SceneManager { get; private set; }


	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(this);
		}
		SceneManager = GetComponentInChildren<SceneManager>();
	}

	public void SetPreviousSceneName(string name)
	{
		previousSceneName = name;
	}
}
