using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : StateMachineBehaviour
{
	Enemy enemy;
	bool hasAttacked;
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		enemy = animator.GetComponent<Enemy>();
		GameObject player = enemy.player;
		hasAttacked = false;
	}


	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.SetBool("isChasing", false);

		Vector2 pos = enemy.enemyPos.position;
		if (enemy.PlayerInEnemyAttackRange(pos) && !hasAttacked && enemy.isFacingPlayer(pos, enemy.player.transform.position))
		{
			enemy.Attack();
			enemy.timeBtwAttacks = enemy.starttimeBtwAttacks;
			hasAttacked = true;
		}
		else
		{
			enemy.timeBtwAttacks -= Time.deltaTime;
		}

		if (hasAttacked && enemy.timeBtwAttacks <= 0)
		{
			Debug.Log("isAttacking false");
			animator.SetBool("isAttacking", false);

		}

	}


	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove()
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    // Implement code that processes and affects root motion
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK()
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    // Implement code that sets up animation IK (inverse kinematics)
	//}
}
