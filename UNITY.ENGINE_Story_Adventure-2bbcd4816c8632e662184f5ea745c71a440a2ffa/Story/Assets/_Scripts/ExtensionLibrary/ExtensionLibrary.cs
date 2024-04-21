using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class ExtensionLibrary
{
	public static bool IsMouseOverUI()
	{
		return EventSystem.current.IsPointerOverGameObject();
	}

	public static void RunActions(Actions[] actions)
	{
		for (int i = 0; i < actions.Length; i++)
		{
			actions[i].Act();
		}
	}
}
