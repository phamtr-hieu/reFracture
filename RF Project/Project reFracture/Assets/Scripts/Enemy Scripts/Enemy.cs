using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	//Enemy Stat
	public float healthPoints = 1000;

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
	[SerializeField] Animator anim;

	public Slider slider;
	[SerializeField] SpriteRenderer sr;
	[SerializeField] DamageFlashing damageFlashing;

	public bool facingLeft = true;
	// Start is called before the first frame update
	void Start()
	{
		anim.GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player");
		damageFlashing.GetComponent<DamageFlashing>();
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
		RaycastHit2D hit = Physics2D.BoxCast(hitboxPos.position, hitboxSize,0,transform.position);
		Debug.LogWarning("Enemy hit:" + hit);
		if (hit)
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

	public void TakeDamage(float damage, float damageFlash)
	{
		if (this != null)
		{
			healthPoints -= damage;
			damageFlashing.DamageFlash(0.1f, sr.material);
			//sr.color = Color.red;
			//IEnumerator coroutine;
			//coroutine = DamageFlash(damageFlash);
			//StartCoroutine(coroutine);
			
		}
	}

	public void Flip()
	{
		this.transform.Rotate(0, 180, 0);
	}

	//IEnumerator DamageFlash(float second)
	//{
	//	yield return new WaitForSeconds(second);
	//	sr.color = Color.white;
	//}
}
