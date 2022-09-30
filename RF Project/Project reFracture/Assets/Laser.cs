using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : StateMachineBehaviour
{
	Enemy enemy;
	GameObject character;
	[SerializeField] Vector2 hitboxPos;
	[SerializeField] Vector2 hitbox;
	[SerializeField] float damage;

	[SerializeField] float beginTime, endTime, tickRate;

	float timer;
	int frameTimer = 0;

	int counter = 0;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		enemy = animator.GetComponent<Enemy>();
		GameObject player = enemy.player;
		character = GameObject.FindGameObjectWithTag("Player");
		enemy.hitboxPos.localPosition = hitboxPos;
		enemy.hitboxSize = hitbox;

		timer = 0;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Collider2D hit = Physics2D.OverlapBox(enemy.hitboxPos.position, enemy.hitboxSize, 0, LayerMask.GetMask("Player"));

		timer += Time.deltaTime;
		frameTimer++;

		if (timer > beginTime && frameTimer % tickRate == 0 && timer < endTime)
		{
			Debug.Log("ticking: " + counter);
			counter++;
			if (hit != null)
			{
				if (hit.CompareTag("Player"))
				{
					character.GetComponent<Character>().TakeDamage(damage);
					Debug.Log("Laser hit " + hit + " for " + damage + " damage");
				}
			}
		}
	}
}
