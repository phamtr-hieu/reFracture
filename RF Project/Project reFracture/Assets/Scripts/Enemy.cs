using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public GameObject player;
	public float chaseDistance;
	public float playerToEnemyDistance;
	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update()
	{
		print(playerToEnemyDistance);
	}

	public bool PlayerInEnemyRange(Vector2 enemy)
	{
		playerToEnemyDistance = Vector2.Distance(enemy, player.transform.position);
		

		if (playerToEnemyDistance > chaseDistance)
		{
			return false;
		}
		else
			return true;

	}


}
