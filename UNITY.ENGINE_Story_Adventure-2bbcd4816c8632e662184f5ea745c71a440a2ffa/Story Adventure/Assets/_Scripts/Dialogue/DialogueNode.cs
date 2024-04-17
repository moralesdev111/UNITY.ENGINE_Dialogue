using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DialogueNode : ScriptableObject
	{
		[SerializeField] private bool isPlayerSpeaking = false;
		[SerializeField] private string text;
		[SerializeField] private List<string> children = new List<string>();
		private Rect rect = new Rect(0,0, 200, 100);
		[SerializeField] private bool reverseDialogueToStartAfterNode = false;
		public bool ReverseDialogueToStartAfterNode { get { return reverseDialogueToStartAfterNode; } }
		[SerializeField] private bool isMonologue = false;
		public bool IsMonologue { get {  return isMonologue; } }

		public Rect GetRect() { return rect; }
		public string GetText() { return text; }
		public List<string> GetChildren() { return children; }

#if UNITY_EDITOR
		public void SetPosition(Vector2 newPosition)
		{
			Undo.RecordObject(this, "Move Dialogue Node"); // set editor dirty
			rect.position = newPosition;
			EditorUtility.SetDirty(this);
		}

		public void SetText(string newText)
		{
			if(newText != text)
			{
				Undo.RecordObject(this, "Update Dialogue Text");
				text = newText;
				EditorUtility.SetDirty(this);
			}
		}

		public void AddChild(string childID)
		{
			Undo.RecordObject(this, "Add Dialogue Link");
			children.Add(childID);
			EditorUtility.SetDirty(this);
		}

		public void RemoveChild(string childID)
		{
			Undo.RecordObject(this, "Remove Dialogue Link");
			children.Remove(childID);
			EditorUtility.SetDirty(this);
		}

		public bool GetIsPlayerSpeaking()
		{
			return isPlayerSpeaking;
		}

		public void SetPlayerSpeaking(bool newIsPlayerSpeaking)
		{
			Undo.RecordObject(this, "Change Dialogue Speaker");
			isPlayerSpeaking = newIsPlayerSpeaking;
			EditorUtility.SetDirty(this);
		}
#endif
	}
	


