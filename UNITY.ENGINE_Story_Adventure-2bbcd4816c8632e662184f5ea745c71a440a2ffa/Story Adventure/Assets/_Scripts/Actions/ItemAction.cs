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

	public void ChangeItem(Item item)
	{
		if(currentItem.itemID == item.itemID)
		{
			return;
		}
		currentItem.itemID = item.itemID;
	}
	public override void Act()
	{
		if(playerGivesItemAway) // give item
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
		else // receive item
		{
			DataManager.Instance.Inventory.AddItemToInventory(currentItem);
		}
	}

}
