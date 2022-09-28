using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
	#region Floats
	//Enemy Stat
	[Header("Enemy Stats")]
	public float healthPoints;
	public float chaseDistance;
	public float playerToEnemyDistance;
	public float chaseSpeed;
	public float atkRange;
	public float timeBtwAttacks;
	public float startTimeBtwAttacks;
	[SerializeField] float idleTime;
	[SerializeField] float currentIdleTime;
	[SerializeField] float enemyDamage;

	[Header("Attacks Cooldowns")]
	[SerializeField] float MaxClawCooldown;
	[SerializeField] float _clawCooldown;
	[Space]
	[SerializeField] float MaxBiteCooldown;
	[SerializeField] float _biteCooldown;
	[Space]
	[SerializeField] float MaxLaserCooldown;
	[SerializeField] float MinLaserCooldown;
	[SerializeField] float _laserCooldown;
	[Space]

	#endregion

	[SerializeField] Character character;
	public GameObject player;

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
		//if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
		//{
		//	idleTime -= Time.deltaTime;
		//}

		#region Cooldown Handling
		if (_clawCooldown > 0)
			_clawCooldown -= Time.deltaTime;

		if (_biteCooldown > 0)
			_biteCooldown -= Time.deltaTime;

		if (_laserCooldown > 0)
			_laserCooldown -= Time.deltaTime;
		#endregion


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

		if (playerToEnemyDistance < 6 && _clawCooldown <= 0)
		{
			anim.SetTrigger("Claw");
			_clawCooldown = Random.Range(0, MaxClawCooldown);
			return;
		}
		if (playerToEnemyDistance >= 8 && playerToEnemyDistance <= 12 && _biteCooldown <= 0)
		{
			anim.SetTrigger("Bite");
			_biteCooldown = Random.Range(0, MaxBiteCooldown);
			return;
		}
		if (playerToEnemyDistance >= 12 && playerToEnemyDistance <= 14)
		{
			anim.SetTrigger("Laser");
			_laserCooldown = Random.Range(MinLaserCooldown, MaxLaserCooldown);
			return;
		}

		//switch (playerToEnemyDistance)
		//{
		//	case <=8 && playerToEnemyDistance <=12:
		//		int attack = Random.Range(0,2);
		//		Debug.Log(attack);
		//		if(attack == 0)
		//		anim.SetTrigger("Claw");
		//		else
		//		anim.SetTrigger("Bite");
		//		break;
		//	case > 12:
		//		anim.SetTrigger("Laser");
		//		break;
		//}
	}

	public void OnDeath()
	{
		anim.SetTrigger("Die");
		Destroy(slider);
		print(anim.GetCurrentAnimatorStateInfo(0).length);
		Destroy(this.gameObject, 7);
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

	public IEnumerator Stopping(float time)
	{
		yield return new WaitForSeconds(time);
		this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}


}
