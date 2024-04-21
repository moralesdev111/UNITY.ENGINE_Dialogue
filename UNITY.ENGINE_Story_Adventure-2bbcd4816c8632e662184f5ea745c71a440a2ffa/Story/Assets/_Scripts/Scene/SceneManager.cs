using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneManager : MonoBehaviour
{
	[SerializeField] private GameObject panel;
	[SerializeField] private RectTransform loadBar;
	private Vector3 loadbarScale = Vector3.one;

	public void LoadScene(string sceneName)
	{
		StartCoroutine(AsyncLoading(sceneName));
	}

	private void Start()
	{
		HidePanel();
	}
	
	IEnumerator AsyncLoading(string sceneName)
	{
		ShowPanel();
		yield return new WaitForEndOfFrame();

		AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
		
		while(!asyncLoad.isDone)
		{
			float progress = Mathf.Clamp01(asyncLoad.progress/0.9f);
			UpdateBar(progress);
			yield return null;
		}
		HidePanel();
	}

	void UpdateBar(float value)
	{
		loadbarScale.x = value;
		loadBar.localScale = loadbarScale;
	}

	void ShowPanel()
	{
		panel.SetActive(true);
	}

	void HidePanel()
	{
		panel.SetActive(false);
	}
}
