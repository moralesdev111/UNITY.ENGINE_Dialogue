using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image slotImage;
    [SerializeField] private Item slotItem;
	[SerializeField] private Transform toolTip;
	[SerializeField] private TextMeshProUGUI toolTipText;

	public void UpdateSlotUIOnNewItem(Item newItem)
	{
		ClearItem();
		slotItem = newItem;
        slotImage.sprite = newItem.itemSprite;
	}

	public void ClearItem()
	{
		if(slotImage != null && slotItem != null)
		{
			slotItem = null;
			slotImage.sprite = null;
		}		
	}

    public Item GetItemInSlot()
    {
        return slotItem;
    }

	public void OnPointerEnter(PointerEventData eventData)
	{
		SetToolTip();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		DisableToolTip();
	}

	private void SetToolTip()
	{
		if (slotItem != null)
		{
			toolTipText.text = slotItem.itemName;
			toolTip.gameObject.SetActive(true);
		}
	}
	private void DisableToolTip()
	{
		if (slotItem != null)
		{
			toolTip.gameObject.SetActive(false);
		}
	}


	public void OnPointerClick(PointerEventData eventData)
	{
		Dialogue dialogue = GetItemInSlot().dialogue;
		
		if(DataManager.Instance.DialogueCurrentParticipants.PlayerConversant != null)
		{
			DataManager.Instance.DialogueCurrentParticipants.PlayerConversant.StartDialogue(dialogue);
		}		
	}
}
