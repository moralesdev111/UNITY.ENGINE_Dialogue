using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

 public class DialogueCurrentParticipants : MonoBehaviour
    {
        [SerializeField] private GameObject activeNPCSpeaker;
		public GameObject ActiveNPCSpeaker { get { return activeNPCSpeaker; } }
		public List<GameObject> globalNPCSpeakers = new List<GameObject>();
        [SerializeField] private PlayerConversant playerConversant;
        public PlayerConversant PlayerConversant { get {  return playerConversant; } }

	private void Awake()
	{
		if (DataManager.Instance != null)
		{
			DataManager.Instance.SetDialogueCurrentParticipants(this);
		}
	}
	private void Start()
	{
			globalNPCSpeakers.Clear();
			UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
			ResetActiveNPCSpeaker();
			FindGlobalNPCSpeakers();
			playerConversant.onNewDialogue += GetActiveNPCSpeaker;
			playerConversant.onDialogueFinish += ResetActiveNPCSpeaker;		
	}
	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		globalNPCSpeakers.Clear();
		FindGlobalNPCSpeakers();
	}

	private void FindGlobalNPCSpeakers()
	{
		NPC[] npcs = FindObjectsOfType<NPC>();
		for (int i = 0; i < npcs.Length; i++)
		{
			if (npcs[i].GetComponent<DialogueAction>())
			{
				globalNPCSpeakers.Add(npcs[i].gameObject);
			}
		}
	}

	public void GetActiveNPCSpeaker()
		{
			if (globalNPCSpeakers != null)
			{
				for (int i = 0; i < globalNPCSpeakers.Count; i++)
				{
					DialogueAction instanceDialogueAction = globalNPCSpeakers[i].GetComponent<DialogueAction>();
					Dialogue instanceDialogue = instanceDialogueAction.Dialogue;

					if (instanceDialogue == playerConversant.CurrentDialogue)
					{
						activeNPCSpeaker = globalNPCSpeakers[i];
						return;
					}
				}
			}
		}

		public void ResetActiveNPCSpeaker()
		{
			 if(activeNPCSpeaker != null)
			{
				activeNPCSpeaker = null;
			}			
		}
	}

