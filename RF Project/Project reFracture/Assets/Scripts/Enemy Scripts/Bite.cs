using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : StateMachineBehaviour
{
	Enemy enemy;
	GameObject character;
	[SerializeField] Vector2 hitboxPos;
	[SerializeField] Vector2 hitbox;
	[SerializeField] float damage;
	[SerializeField] float lungeDistance;
	[SerializeField] float waitForAnimTime;
	
	float waitForAnim;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		enemy = animator.GetComponent<Enemy>();
		character = GameObject.FindGameObjectWithTag("Player");
		enemy.hitboxPos.localPosition = hitboxPos;
		enemy.hitboxSize = hitbox;
		waitForAnim = waitForAnimTime;
	}


	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		waitForAnim -= Time.deltaTime;
		Vector2 pos = enemy.transform.position;
		if (enemy.PlayerInEnemyAttackRange(pos) && enemy.isFacingPlayer(pos, enemy.player.transform.position) && waitForAnim <= 0)
		{
			
			Lunge();


		}
	}

	void Lunge()
	{
		switch (enemy.transform.InverseTransformDirection(character.transform.position).x)
		{
			case > 0:
				enemy.GetComponent<Rigidbody2D>().velocity = new Vector3(-lungeDistance, 0);
				break;
			case < 0:
				enemy.GetComponent<Rigidbody2D>().velocity = new Vector3(lungeDistance, 0);
				break;
			case 0:
				break;
		}


		RaycastHit2D hit = Physics2D.BoxCast(hitboxPos, hitbox, 0, enemy.transform.position);
		if (hit)
		{
			character.GetComponent<Character>().TakeDamage(damage);
			Debug.Log("Bite hit" + hit + "for" + damage + "damage");
		}
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

	}
}


