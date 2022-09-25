using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
	//Enemy Stat
	public float healthPoints;

	[SerializeField] Character character;
	public GameObject player;


	[SerializeField] Vector3 point;
	#region Floats
	public float chaseDistance;
	public float playerToEnemyDistance;
	public float chaseSpeed;
	public float atkRange;
	public float timeBtwAttacks;
	public float startTimeBtwAttacks;
	[SerializeField] float idleTime;
	[SerializeField] float currentIdleTime;
	[SerializeField] float enemyDamage;
	#endregion


	[SerializeField] public Transform hitboxPos;
	public Transform enemyPos;
	[SerializeField] public Vector2 hitboxSize;
	[SerializeField] Animator anim;
	public Slider slider;
	[SerializeField] SpriteRenderer sr;
	[SerializeField] DamageFlashing damageFlashing;

	public bool facingLeft = true;
	// Start is called before the first frame update
	void Start()
	{
		anim = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player");
		damageFlashing.GetComponent<DamageFlashing>();
		slider.maxValue = healthPoints;
		currentIdleTime = idleTime;
	}



	// Update is called once per frame
	void Update()
	{
		point = transform.InverseTransformDirection(character.transform.position);
		slider.value = healthPoints;

		if (healthPoints <= 0)
		{
			OnDeath();
		}

		if (Keyboard.current.slashKey.isPressed)
		{
			TakeDamage(0.01f, 0.1f);
		}

		//Countdown Idle time
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
		{
			idleTime -= Time.deltaTime;
		}

		
	}

	public bool PlayerInEnemyChaseRange(Vector2 enemy)
	{
		playerToEnemyDistance = Vector2.Distance(enemy, player.transform.position);


		if (playerToEnemyDistance <= chaseDistance && currentIdleTime <= 0)
		{
			return true;
		}
		else
			return false;



	}

	public bool PlayerInEnemyAttackRange(Vector2 enemy)
	{
		playerToEnemyDistance = Vector2.Distance(enemy, player.transform.position);


		if (playerToEnemyDistance <= atkRange && currentIdleTime <= 0)
		{
			return true;
		}
		else
			return false;

	}

	public bool isFacingPlayer(Vector2 enemy, Vector2 player)
	{
		if (player.x < enemy.x)
		{
			if (facingLeft)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (player.x > enemy.x)
		{
			if (!facingLeft)
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
		//RaycastHit2D hit = Physics2D.BoxCast(hitboxPos.position, hitboxSize, 0, transform.position);
		//Debug.LogWarning("Enemy hit:" + hit);
		//if (hit)
		//{
		//	character.healthPoint -= enemyDamage;
		//	print("player took damage");
		//}
	}

	public void ChooseNextAttack(Vector2 enemy)
	{
		playerToEnemyDistance = Vector2.Distance(enemy, player.transform.position);


		switch (playerToEnemyDistance)
		{
			case >16:
				anim.SetTrigger("Bite");
				break;
			case <=8:
				anim.SetTrigger("Claw");
				break;
		}



		//anim.SetTrigger("Laser");

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
		//Gizmos.DrawWireSphere(hitboxPos.position, hitboxSize.x);
	}

	public void TakeDamage(float damage, float damageFlash)
	{
		if (this != null)
		{
			healthPoints -= damage;
			damageFlashing.DamageFlash(0.1f);


		}
	}

	public void Flip()
	{
		this.transform.Rotate(0, 180, 0);
	}


}
