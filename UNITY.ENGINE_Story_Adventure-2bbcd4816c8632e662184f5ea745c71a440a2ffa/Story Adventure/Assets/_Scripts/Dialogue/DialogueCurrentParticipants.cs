using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class DialogueCurrentParticipants : MonoBehaviour
    {
        private GameObject activeNPCSpeaker;
		public GameObject ActiveNPCSpeaker { get { return activeNPCSpeaker; } }
		[SerializeField] private List<GameObject> globalNPCSpeakers = new List<GameObject>();
        [SerializeField] private PlayerConversant playerConversant;
        public PlayerConversant PlayerConversant { get {  return playerConversant; } }

		private void Start()
		{
            playerConversant.onNewDialogue += GetActiveNPCSpeaker;
		}

		public void GetActiveNPCSpeaker()
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

