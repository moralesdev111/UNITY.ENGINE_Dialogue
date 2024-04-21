using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneAction : Actions
{
	[SerializeField] string sceneTarget;

	// Update is called once per frame
	public override void Act()
	{
		DataManager.Instance.SetPreviousSceneName(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
		DataManager.Instance.SceneManager.LoadScene(sceneTarget);
	}
}
