using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSceneSetter : MonoBehaviour
{
    [SerializeField] private GameObject DDoL;

	private void Awake()
	{
		DDoL[] existingDDoLs = FindObjectsOfType<DDoL>();
		if(existingDDoLs.Length == 0)
		{
			Instantiate(DDoL, Vector3.zero, Quaternion.identity);
		}
	}
}
