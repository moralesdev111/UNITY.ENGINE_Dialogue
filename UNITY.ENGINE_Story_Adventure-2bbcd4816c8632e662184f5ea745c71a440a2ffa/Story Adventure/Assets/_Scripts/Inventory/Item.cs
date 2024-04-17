using System.Collections;
using System.Collections.Generic;
using UnityEngine;


	[CreateAssetMenu(fileName = "Item")]
	public class Item : ScriptableObject
	{
		public int itemID;
		public string itemName;
		public Sprite itemSprite;
		public GameObject item3DObject;
		public Dialogue dialogue;
}

