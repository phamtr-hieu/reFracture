using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	PlayerLoadout loadout;
	void Start()
	{
		loadout = GetComponent<PlayerLoadout>();
	}

	void Update()
	{

	}


	public void LoadNextLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void LoadCardsAndNextLevel()
	{
		if (!loadout.hasLoadout)
		{
			loadout.WriteAttackList();
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
		else
		{
			loadout.OverwriteAttackList();
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			
		}

	}
}
