using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PickupAction : Actions
{
	// Start is called before the first frame update
	[SerializeField] private ItemPickup itemPickup;

	public override void Act()
	{		
		DataManager.Instance.Inventory.AddItemToInventory(itemPickup.Item);
		Destroy(gameObject);		
	}
}
