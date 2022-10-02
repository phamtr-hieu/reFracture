using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLoadout : MonoBehaviour
{
	public static PlayerLoadout instance;

	public int[] attackList;
	[SerializeField] public int attacklistSize = 5;
	[SerializeField] public DropSlot[] dropSlots = new DropSlot[5];
	public bool hasLoadout = false;
	bool dropSlotFilled;

	private void Awake()
	{

		if (instance == null)
			instance = this;
		else
		{
			instance.hasLoadout = true;
			hasLoadout = true;
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		attackList = new int[attacklistSize];
	}

	void Update()
	{
		if(dropSlots  == null)
		{
			dropSlotFilled = false;
		}

		if (hasLoadout && SceneManager.GetActiveScene().buildIndex == 1 && !dropSlotFilled) 
		{
			for (int i = 0; i < attacklistSize; i++)
			{
				dropSlots[i] = GameObject.Find("dropSlot " + i).GetComponent<DropSlot>();
				print("Drop slots filled");
			}
			dropSlotFilled = true;
		}
	}

	public void WriteAttackList()
	{
		if (dropSlots[0].hasData && dropSlots[1].hasData && dropSlots[2].hasData && dropSlots[3].hasData && dropSlots[4].hasData)
		{
			for (int i = 0; i < attacklistSize; i++)
			{
				attackList[i] = dropSlots[i].cardData.cardID;
			}
			hasLoadout = true;
		}
	}

	//public void OverwriteAttackList()
	//{
	//	if (dropSlots[0].hasData && dropSlots[1].hasData && dropSlots[2].hasData && dropSlots[3].hasData && dropSlots[4].hasData)
	//	{
	//		for (int i = 0; i < attacklistSize; i++)
	//		{
	//			instance.attackList[i] = dropSlots[i].cardData.cardID;
	//		}
	//		print("Loadout Overwritten");
	//	}

	//}
}
