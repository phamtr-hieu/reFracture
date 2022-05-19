using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] Character character;
	public GameObject player;
	public float chaseDistance;
	public float playerToEnemyDistance;
	public float chaseSpeed;
	public float atkRange;
	public float timeBtwAttacks;
	public float starttimeBtwAttacks;
	[SerializeField] float enemyDamage;
	[SerializeField] Transform hitboxPos;
	[SerializeField] Vector2 hitboxSize;
	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update()
	{
		//print(playerToEnemyDistance);
	}

	public bool PlayerInEnemyChaseRange(Vector2 enemy)
	{
		playerToEnemyDistance = Vector2.Distance(enemy, player.transform.position);
		

		if (playerToEnemyDistance > chaseDistance)
		{
			return false;
		}
		else
			return true;

	}

	public bool PlayerInEnemyAttackRange(Vector2 enemy)
	{
		playerToEnemyDistance = Vector2.Distance(enemy, player.transform.position);


		if (playerToEnemyDistance <= atkRange)
		{
			return true;
		}
		else
			return false;

	}

	public void Attack()
	{
		bool hit = Physics2D.OverlapBox(hitboxPos.position,hitboxSize,0);
		if(hit)
		{
			character.healthPoint -= enemyDamage;
			print("player took damage");
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(hitboxPos.position, hitboxSize);
	}


}
