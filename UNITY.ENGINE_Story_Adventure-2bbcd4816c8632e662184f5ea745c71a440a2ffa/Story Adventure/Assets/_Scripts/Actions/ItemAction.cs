using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAction : Actions
{
    [SerializeField] private bool playerGivesItemAway;
    [SerializeField] Actions[] yesActions, noActions;
    [SerializeField] private Item currentItem;
	public Item CurrentItem { get { return currentItem; } }
	public int itemID;

	public override void Act()
	{
		if(playerGivesItemAway) // player gives item
		{
			if(DataManager.Instance.Inventory.CheckIfHasItem(currentItem))
			{
				DataManager.Instance.Inventory.RemoveItemFromInventory(currentItem);
				ExtensionLibrary.RunActions(yesActions);
			}
			else
			{
				ExtensionLibrary.RunActions(noActions);
			}
		}
		else // player receives item
		{
			DataManager.Instance.Inventory.AddItemToInventory(currentItem);
		}
	}

}
