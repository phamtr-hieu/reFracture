using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
	[SerializeField] float animTimer;
		public void Play()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void Credits()
	{
		SceneManager.LoadScene("Credits");
	}

	public void Title()
	{
		SceneManager.LoadScene(0);
	}


	private void Start()
	{
		
		if (SceneManager.GetActiveScene().buildIndex == 3)
		{
			StartCoroutine(LoadDeathScene());
		}
	}

	private void Update()
	{
		animTimer -= Time.deltaTime;
		
	}

	IEnumerator LoadDeathScene()
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
		asyncLoad.allowSceneActivation = false;
		yield return new WaitForSeconds(animTimer);
		asyncLoad.allowSceneActivation = true;


		
	}

}
