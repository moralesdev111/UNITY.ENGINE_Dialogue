using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
	private Inventory inventory;
	[SerializeField] private Transform slotParent;
	[SerializeField] private GameObject slotPrefab;
	private InventorySlot[] slots;
	[SerializeField] private Image inventoryBackgroundUI;
	public Image InventorybackgroundUI { get { return inventoryBackgroundUI; } }
	[SerializeField] private Button inventoryToggleButton;
	

	void Start()
	{
		inventory = DataManager.Instance.Inventory;
		if(inventory != null )
		{
			inventory.uiChangeTriggered += UpdateUI;
			inventoryToggleButton.onClick.AddListener(ToggleInventoryUI);
			InstantiateInventorySlots();
		}
	}

	void InstantiateInventorySlots()
	{
		slots = new InventorySlot[inventory.InventorySpace];
		for (int i = 0; i < inventory.InventorySpace; i++)
		{
			GameObject slotObject = Instantiate(slotPrefab, slotParent);
			slots[i] = slotObject.GetComponent<InventorySlot>();
		}
		ClearAllSlots();
	}

	void UpdateUI()
	{
		ClearAllSlots();
		for (int i = 0; i < inventory.Items.Count && i < slots.Length; i++)
		{
			slots[i].UpdateSlotUIOnNewItem(inventory.Items[i]);
		}
	}
	void ClearAllSlots()
	{
		for (int i = 0; i < slots.Length; i++)
		{
			slots[i].ClearItem();
		}
	}

	void ToggleInventoryUI()
	{
		if(inventoryBackgroundUI.gameObject.activeInHierarchy)
		{
			inventoryBackgroundUI.gameObject.SetActive(false);
		}
		else
		{
			inventoryBackgroundUI.gameObject.SetActive(true);
		}
	}
}
