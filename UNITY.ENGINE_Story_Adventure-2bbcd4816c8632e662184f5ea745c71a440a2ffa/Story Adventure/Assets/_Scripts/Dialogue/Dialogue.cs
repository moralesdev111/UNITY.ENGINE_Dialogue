using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 0)]
public class Dialogue : ScriptableObject, ISerializationCallbackReceiver
{
	[SerializeField] private List<DialogueNode> nodes = new List<DialogueNode>();
	private Vector2 newNodeOffsetVector = new Vector2(250f, 0f);
	private Dictionary<string, DialogueNode> nodeLookupDictionary = new Dictionary<string, DialogueNode>();


	private void OnValidate()
	{
		nodeLookupDictionary.Clear();
		foreach (DialogueNode node in GetAllNodes())
		{
			if (node != null)
			{
				nodeLookupDictionary[node.name] = node;
			}
		}
	}

	public IEnumerable<DialogueNode> GetAllNodes()
	{
		return nodes;
	}

	public DialogueNode GetRootNode()
	{
		return nodes[0];
	}

	public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parentNode)
	{
		foreach (string childName in parentNode.GetChildren())
		{
			if (nodeLookupDictionary.ContainsKey(childName))
			{
				yield return nodeLookupDictionary[childName];
			}
		}
	}

	public IEnumerable<DialogueNode> GetPlayerChildren(DialogueNode currentNode)
	{
		foreach (DialogueNode node in GetAllChildren(currentNode))
		{
			if (node.GetIsPlayerSpeaking())
			{
				yield return node;
			}
		}
	}

	public IEnumerable<DialogueNode> GetNPCChildren(DialogueNode currentNode)
	{
		foreach (DialogueNode node in GetAllChildren(currentNode))
		{
			if (!node.GetIsPlayerSpeaking())
			{
				yield return node;
			}
		}
	}
#if UNITY_EDITOR
	public void CreateNode(DialogueNode parent)
	{
		DialogueNode newNode = MakeNode(parent);
		Undo.RegisterCreatedObjectUndo(newNode, "Created Dialogue Node");
		Undo.RecordObject(this, "Added Dialogue Node");
		AddNode(newNode);
	}

	public void DeleteNode(DialogueNode nodeToDelete)
	{
		Undo.RecordObject(this, "Deleted Dialogue Node");
		nodes.Remove(nodeToDelete);
		OnValidate();
		CleanDanglingChildren(nodeToDelete);
		Undo.DestroyObjectImmediate(nodeToDelete);
	}
	private DialogueNode MakeNode(DialogueNode parent)
	{
		DialogueNode newNode = CreateInstance<DialogueNode>();
		newNode.name = Guid.NewGuid().ToString();

		if (parent != null)
		{
			parent.AddChild(newNode.name);
			newNode.SetPlayerSpeaking(!parent.GetIsPlayerSpeaking());
			newNode.SetPosition(parent.GetRect().position + newNodeOffsetVector);
		}
		return newNode;
	}

	private void AddNode(DialogueNode newNode)
	{
		nodes.Add(newNode);
		OnValidate();
	}

	private void CleanDanglingChildren(DialogueNode nodeToDelete)
	{
		foreach (DialogueNode node in GetAllNodes())
		{
			node.RemoveChild(nodeToDelete.name);
		}
	}
#endif
	public void OnBeforeSerialize()
	{
#if UNITY_EDITOR
		if (nodes.Count == 0)
		{
			DialogueNode newNode = MakeNode(null);
			AddNode(newNode);
		}

		if (AssetDatabase.GetAssetPath(this) != "")
		{
			foreach (DialogueNode node in GetAllNodes())
			{
				if (AssetDatabase.GetAssetPath(node) == "")
				{
					AssetDatabase.AddObjectToAsset(node, this);
				}
			}
		}
#endif
	}

	public void OnAfterDeserialize()
	{
	}
}