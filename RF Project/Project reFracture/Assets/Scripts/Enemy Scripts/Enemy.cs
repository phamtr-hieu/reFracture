using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	//Enemy Stat
	public float healthPoints = 100;

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
	public Transform enemyPos;
	[SerializeField] Vector2 hitboxSize;

	public Slider slider;

	public bool facingLeft = true;
	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		slider.maxValue = healthPoints;
	}

	// Update is called once per frame
	void Update()
	{
		slider.value = healthPoints;

		if (healthPoints <= 0)
		{
			OnDeath();
		}
		

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

	public bool isFacingPlayer(Vector2 enemy, Vector2 player)
	{
		if(player.x < enemy.x)
		{
			if(facingLeft)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if(player.x > enemy.x)
		{
			if(!facingLeft)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else
		{
			return true;
		}
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

	public void OnDeath()
	{

		Destroy(slider);
		Destroy(this.gameObject);
		//print("Enemy Died");

	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(hitboxPos.position, hitboxSize);
	}

	public void Flip()
	{
		this.transform.Rotate(0, 180, 0);
	}

}
