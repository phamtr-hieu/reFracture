using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void Play()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void Quit()
	{
		Application.Quit();
	}

	private void Update()
	{
		if(SceneManager.GetActiveScene().buildIndex ==3)
		{
			print("die");
			Invoke("LoadDeathScene",2.3f);
		}
	}

	void LoadDeathScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}	

}
