using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerAttackManager : MonoBehaviour
{

	//generate list of random attacks from attack deck
	//(remove attacks as they are drawn from the array)
	public int numberOfCards;
	public TextMeshProUGUI[] card = new TextMeshProUGUI[3];
	public CardDisplay[] cardDisplays = new CardDisplay[3];

	int[] attackList = new int[4];

	public int[] attackQueue = new int[4];

	int nextAttackID;

	private void Start()
	{
		//setting player's possible attacks 
		// 0 = null
		//todo: optimize and automate this process based on some datasheet
		attackList[0] = 1;
		attackList[1] = 2;
		attackList[2] = 3;
		attackList[3] = 4;

		//sets all attacks to 0 (no attacks)
		for (int i = 0; i < attackQueue.Length; i++)
		{
			attackQueue[i] = 0;
		}

		//run init code for attack queue
		InitAttackQueue();
	}

	private void Update()
	{
		DisplayAttackID();
		if (UnityEngine.InputSystem.Keyboard.current.rKey.wasPressedThisFrame)
		{
			SceneManager.LoadScene(0);
		}


	}

	public void OnAttack()
	{
		attackQueue[0] = 0;

		UpdateAttackQueue();
		GenerateNextAttack();
		SortAttackStackID(nextAttackID);
	}

	void InitAttackQueue()
	{
		for (int i = 0; i < attackQueue.Length; i++)
		{
			if (attackQueue[i] < 1)
			{
				GenerateNextAttack();
				SortAttackStackID(nextAttackID);
			}
		}
	}

	void GenerateNextAttack()
	{
		//todo: add sorting system to cycle through a list of attacks without repeating
		nextAttackID = Random.Range(1, 5);
	}

	void SortAttackStackID(int attackID)
	{
		//check for empty slots
		int stack = 0;
		foreach (int i in attackQueue)
		{
			if (i > 0)
				stack++;
		}

		attackQueue[stack] = attackID;
	}

	void UpdateAttackQueue()
	{
		for (int i = 0; i < attackQueue.Length; i++)
		{
			if (i < attackQueue.Length - 1)
			{
				if (attackQueue[i] < 1 && attackQueue[i + 1] != 0)
				{
					attackQueue[i] = attackQueue[i + 1];
					attackQueue[i + 1] = 0;
				}
			}
			else if (i == attackQueue.Length - 1)
			{
				attackQueue[i] = 0;
			}
		}
	}

	void DisplayAttackID()
	{
		for (int i = 0; i < attackQueue.Length; i++)
		{
			//Debug.LogWarning(i);
			card[i].text = attackQueue[i].ToString();
			cardDisplays[i].cardID = attackQueue[i];
		}
	}
}
