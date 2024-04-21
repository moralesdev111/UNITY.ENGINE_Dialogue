using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour, IInteracteable
{
	[SerializeField] private Item item;
	[SerializeField] private Image toolTip;
	private TextMeshProUGUI toolTipText;

	private void Start()
	{
		this.name = item.name;
		GetGrandChildComponents();
	}

	public void OnMouseOver()
	{
		Interact();
	}

	public void OnMouseExit()
	{
		toolTip.gameObject.SetActive(false);
	}

	public void Interact()
	{
		HandleToolTipOnHover();
		if (Input.GetMouseButtonDown(0))
		{
			DataManager.Instance.Inventory.AddItemToInventory(item);
			Destroy(gameObject);
		}
	}

	private void HandleToolTipOnHover()
	{
		toolTipText.text = item.itemName;
		toolTip.gameObject.SetActive(true);
		toolTip.gameObject.transform.position = Input.mousePosition + new Vector3(25, 25, 0);
	}

	private void GetGrandChildComponents()
	{
		Transform grandChild = transform.GetChild(0).GetChild(0);
		toolTip = grandChild.GetComponent<Image>();
		Transform greatGrandChild = grandChild.GetChild(0);
		toolTipText = greatGrandChild.GetComponent<TextMeshProUGUI>();
	}
}