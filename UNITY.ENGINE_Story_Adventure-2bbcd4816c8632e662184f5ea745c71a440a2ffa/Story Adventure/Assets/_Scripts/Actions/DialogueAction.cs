using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAction : Actions
	{
		[SerializeField] private Dialogue dialogue;
		public Dialogue Dialogue { get { return dialogue; } }
		[SerializeField] private PlayerConversant playerConversant;

		public override void Act()
		{
			if(dialogue != null)
			{
				playerConversant.StartDialogue(dialogue);
			}
		}
	}


